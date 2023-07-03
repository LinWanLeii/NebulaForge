using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInverseKinematics : MonoBehaviour
{
    public int numChains;
    public Transform targetTransform;
    public Transform poleTransform;
    public Transform[] bones;
    public Vector3[] positions;
    public float[] boneLengths;
    public float completeChainLength;

    // Calculation of IK
    public int numCalIterations;
    public float errorMargin;
    public float snapBackStrength;

    public Vector3[] startDirection;
    public Quaternion[] startRotationBone;
    public Quaternion startRotationTarget;
    public Quaternion startRotationRoot;

    // Start is called before the first frame update
    void Start()
    {
        InitArrays();
    }

    // Update is called once per frame
    void Update()
    {
        DebugVisualizer();
        CalculateIK();
    }

    void InitArrays() {
        if (targetTransform == null) {
            return;
        }
        
        bones = new Transform[numChains + 1];
        positions = new Vector3[numChains + 1];
        boneLengths = new float[numChains];

        startDirection = new Vector3[numChains + 1];
        startRotationBone = new Quaternion[numChains + 1];

        completeChainLength = 0;
        startRotationTarget = targetTransform.rotation;

        // Loop through
        Transform curr = this.transform;
        for (int i = numChains; i >= 0; i--) {
            bones[i] = curr;
            startRotationBone[i] = curr.rotation;

            if (i == numChains) {
                startDirection[i] = targetTransform.position - curr.position;
            } else {
                startDirection[i] = bones[i + 1].position - curr.position;
                boneLengths[i] = startDirection[i].magnitude;
                completeChainLength += boneLengths[i];
            }
            
            curr = curr.parent;            
        }
    }

    void CalculateIK() {
        if (targetTransform == null) {
            return;
        }

        if (boneLengths.Length != numChains) {
            InitArrays();
        }

        // Store the positions so calculations can be done without affecting actual pos
        for (int i = 0; i < bones.Length; i++) {
            positions[i] = bones[i].position;
        }

        Quaternion rootRot = Quaternion.identity;
        if (bones[0].parent != null) {
            rootRot = bones[0].parent.rotation;
        }
        Quaternion rotationDifference = rootRot * Quaternion.Inverse(startRotationRoot);

        // Do calculations on stored positons
        // If not possible to reach target vector, just stretch completely
        if ((targetTransform.position - bones[0].position).sqrMagnitude >= completeChainLength * completeChainLength) {
            Vector3 dir = (targetTransform.position - positions[0]).normalized;

            for (int i = 1; i < positions.Length; i++) {
                positions[i] = positions[i - 1] + dir * boneLengths[i - 1];
            }
        } else {
            for (int iter = 0; iter < numCalIterations; iter++) {
                // Calculate pos starting from child to parent
                for (int i = positions.Length - 1; i > 0; i--) {
                    if (i == positions.Length - 1) {
                        positions[i] = targetTransform.position;
                    } else {
                        Vector3 dir = (positions[i] - positions[i + 1]).normalized;
                        positions[i] = positions[i + 1] + dir * boneLengths[i];
                    }
                }
                
                // Calculate pos starting from parent to child
                for (int i = 1; i < positions.Length; i++) {
                    Vector3 dir = (positions[i] - positions[i - 1]).normalized;
                    positions[i] = positions[i - 1] + dir * boneLengths[i - 1];
                }

                // If within error margin aka its close enough
                if ((positions[positions.Length - 1] - targetTransform.position).sqrMagnitude < errorMargin * errorMargin)
                    break;
            }
        }
        
        // Shift calculated positions based on pole vector
        if (poleTransform != null) {
            for (int i = 1; i < positions.Length - 1; i++) {
                Vector3 normal = (positions[i + 1] - positions[i - 1]).normalized;
                Plane p = new Plane(normal, positions[i - 1]);
                Vector3 projectedPole = p.ClosestPointOnPlane(poleTransform.position);
                Vector3 projectedPos = p.ClosestPointOnPlane(positions[i]);
                float angle = Vector3.SignedAngle(projectedPos - positions[i - 1], projectedPole - positions[i - 1], normal);
                positions[i] = Quaternion.AngleAxis(angle, normal) * (positions[i] - positions[i - 1]) + positions[i - 1];
            }
        }


        // Set the new calculated positions and rotations to actual
        for (int i = 0; i < positions.Length; i++) {
            // Set pos
            bones[i].position = positions[i];

            // Set rotation
            if (i == positions.Length - 1) {
                bones[i].rotation = targetTransform.rotation * Quaternion.Inverse(startRotationTarget) * startRotationBone[i];
            } else {
                bones[i].rotation = Quaternion.FromToRotation(startDirection[i], positions[i + 1] - positions[i]) * startRotationBone[i];
            }
        }
    }

    void DebugVisualizer() {
        var current = this.transform;
        for (int i = 0; i < numChains && current != null && current.parent != null; i++) {   
            Debug.DrawLine(current.position, current.parent.position, Color.green);
            current = current.parent;
        }
    }
}
