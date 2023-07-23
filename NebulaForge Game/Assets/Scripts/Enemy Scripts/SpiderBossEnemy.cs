using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemy : MonoBehaviour
{
    private CustomSpiderAnim customAnim;
    public Vector3 playerPos;
    public float damage;
    public float currHealth;
    public float maxHealth;
    public float attackDistance;
    public float attackCooldownTime;
    public float attackCooldownTimer;
    public float chargeSpeed;
    public float damageLingerTime;

    // If I had more time, ill do a state machine, but it is what it is
    // 3 states, chase, charge, fire
    public bool isChasing;
    public bool isCharging;
    public bool isFiring;
    public GameObject attackGO;
    // Start is called before the first frame update
    void Start()
    {
        customAnim = GetComponent<CustomSpiderAnim>();
        isChasing = true;
        currHealth = maxHealth;
        attackCooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (FPSCameraShift.instance.startShift) {return;}
        playerPos = PlayerStats.instance.gameObject.transform.position;

        if (isChasing) {
            // Check if close enough to start attacking
            if (Vector3.Distance(playerPos, customAnim.spiderBody.position) <= attackDistance) {
                isChasing = false;
                isCharging = true;
            } else {
                customAnim.targetLocation.position = playerPos;
            }
        } else if (isCharging) {
            attackGO.GetComponent<SpiderBossEnemyAttack>().StartCharge(chargeSpeed, damageLingerTime, damage);
            isCharging = false;
            isFiring = true;
            attackCooldownTimer = 0;
        } else if (isFiring) {
            if (attackCooldownTimer >= attackCooldownTime) {
                isFiring = false;
                isChasing = true;
            } else {
                attackCooldownTimer += Time.deltaTime;
            }
        }
    }
}
