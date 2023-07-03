using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointTest : MonoBehaviour
{
    public Transform parentJoint;
    public Transform childJoint;
    public bool parentIsLocked;

    public float boneLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 parentPos;
        Vector3 childPos;
        float distance;
        Vector3 direction;
        
        // Joints
        if (!parentIsLocked) {
            parentPos = parentJoint.position;
            childPos = childJoint.position;
            distance = Vector3.Distance(parentPos, childPos);
            direction = (childPos - parentPos).normalized;

            // If child goes too far
            if (boneLength < distance) {
                parentJoint.position += (distance - boneLength) * direction;
                childJoint.position -= (distance - boneLength) * direction;
            }
            // If child goes too close
            else if (boneLength > distance) {
                parentJoint.position -= (boneLength - distance) * direction;
                childJoint.position += (boneLength - distance) * direction;
            }
        }
        else {
            // If child goes too close, move inwards instead of outwards
            
            parentPos = parentJoint.position;
            childPos = childJoint.position;
            distance = Vector3.Distance(parentPos, childPos);
            direction = (childPos - parentPos).normalized;

            // If child goes too far
            if (boneLength < distance) {
                childJoint.position -= (distance - boneLength) * direction;
            }
            // If child goes too close
            else if (boneLength > distance) {
                childJoint.position += (boneLength - distance) * direction;
            }
        }
    
        // Bone
        parentPos = parentJoint.position;
        childPos = childJoint.position;
        distance = Vector3.Distance(parentPos, childPos);
        direction = (childPos - parentPos).normalized;

        this.transform.localPosition = childJoint.localPosition * 0.5f;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }
}
