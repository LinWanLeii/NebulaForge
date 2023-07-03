using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHomingProjectile : PlayerBasicProjectile, IPooledObject
{
    [SerializeField]
    private int maxColliders = 10;
    [SerializeField]
    private Collider[] colliders;
    [SerializeField]
    private Transform homingTarget;
    [SerializeField]
    private bool isHoming;
    [SerializeField]
    private float homingRadius;

    // Start is called before the first frame update
    void Start()
    {
        homingTarget = transform;
        isHoming = false;
        colliders = new Collider[maxColliders];
    }
    
    // Check if there's an enemy nearby
    // Set the nearest enemy as the target for homing projectile
    // Return true if enemy is found
    // Return false otherwise
    bool SeekEnemy(float radius)
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        
        // If nothing is found
        if (numColliders == 0) {
            return false;
        }

        // Look for the closest collider
        float minDist = Mathf.Infinity;
        Transform target = transform;
        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i].tag == "Enemy") {
                float currDist = (colliders[i].transform.position - transform.position).sqrMagnitude;
                if (currDist < minDist) {
                    target = colliders[i].transform;
                    minDist = currDist;
                }
            }
        }

        if (transform != target) {
            homingTarget = target;
            return true;
        }

        return false;
    }

    public override void ProjectileUpdate() {
        transform.position += dir * speed * Time.deltaTime;

        if (!isHoming) {
            // Find a homing target if there isn't one
            isHoming = SeekEnemy(homingRadius);
        } else {
            if (!homingTarget.gameObject.activeSelf) {
                isHoming = false;
            } else {
                // Rotate towards the target if there is one
                dir = Vector3.Lerp(dir, (homingTarget.position - transform.position).normalized, Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(dir, transform.up);
            }

        }
    }

    public override void Reset() {
        gameObject.SetActive(false);
        
        dir = Vector3.zero;
        speed = 0;
        damage = 0;
        
        homingTarget = transform;
        isHoming = false;
        colliders = new Collider[maxColliders];
    }
}
