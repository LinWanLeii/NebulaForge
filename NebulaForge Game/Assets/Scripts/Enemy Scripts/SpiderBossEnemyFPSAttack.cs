using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemyFPSAttack : MonoBehaviour
{
    [SerializeField]
    private AudioSource shootSound;

    [SerializeField]
    private float weaponCooldownTime;
    [SerializeField]
    private float weaponCooldownTimer;
    [SerializeField]
    private float weaponCooldownBurstTime;
    [SerializeField]
    private float weaponCooldownBurstTimer;
    [SerializeField]
    private float weaponDamage;
    [SerializeField]
    private Vector3 weaponShootPos;
    [SerializeField]
    private float weaponShootSpeed;
    [SerializeField]
    private float burstDuration;
    [SerializeField]
    private bool burstReady;


    // Start is called before the first frame update
    void Start()
    {
        burstReady = false;
        weaponCooldownTimer = 0;
        weaponCooldownBurstTimer = 0;
        DefaultSetting();
    }

    void DefaultSetting() {
        burstDuration = 5.0f;
        weaponCooldownBurstTime = 5.0f;
        weaponCooldownTime = 0.2f;
        weaponShootSpeed = 0.5f;
        weaponDamage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (burstReady) {
            // Handle burst duration
            if (weaponCooldownBurstTimer >= burstDuration) {
                burstReady = false;
                weaponCooldownBurstTimer = 0;
            } else {
                weaponCooldownBurstTimer += Time.deltaTime;
            }

            // Handle burst shot
            if (weaponCooldownTimer >= weaponCooldownTime) {
                weaponCooldownTimer = 0;
                shootSound.Play();
                BossProjectilePooler.instance.SpawnFromPool("BossProjectile", transform.position, Quaternion.identity)
                .GetComponent<SpiderBossEnemyFPSProjectile>().Shoot(Vector3.zero,
                                                                    weaponShootSpeed,
                                                                    weaponDamage);
            } else {
                weaponCooldownTimer += Time.deltaTime;
            }
        } else {
            if (weaponCooldownBurstTimer >= weaponCooldownBurstTime) {
                burstReady = true;
                weaponCooldownBurstTimer = 0;
            } else {
                weaponCooldownBurstTimer += Time.deltaTime;
            }

        }
        
    }
}
