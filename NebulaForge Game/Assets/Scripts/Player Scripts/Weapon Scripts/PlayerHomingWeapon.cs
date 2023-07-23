using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingWeapon : SkillUpgradeBehaviour
{
    override public void Upgrade(SkillUpgrade _skillUpgrade) {
        if (_skillUpgrade.sName == "Faster rocket reload") {
            const float MIN_SHOOT_COOLDOWN = 0.1f;
            const float COOLDOWN_UPGRADE = 0.1f;

            weaponCooldownTime -= COOLDOWN_UPGRADE;
            if (weaponCooldownTime <= MIN_SHOOT_COOLDOWN) {
                weaponCooldownTime = MIN_SHOOT_COOLDOWN;
                _skillUpgrade.sMaxed = true;
            }
        }
        else if (_skillUpgrade.sName == "Aerodynamic rockets") {
            const float MAX_SHOOT_SPEED = 100.0f;
            const float SHOOT_UPGRADE = 20.0f;

            weaponShootSpeed += SHOOT_UPGRADE;
            if (weaponCooldownTime >= MAX_SHOOT_SPEED) {
                weaponCooldownTime = MAX_SHOOT_SPEED;
                _skillUpgrade.sMaxed = true;
            }
        }
        else if (_skillUpgrade.sName == "Heavy duty rockets") {
            const float DAMAGE_UPGRADE = 5.0f;

            weaponShootSpeed += DAMAGE_UPGRADE;
        }
    }

    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private float weaponCooldownTime;
    [SerializeField]
    private float weaponCooldownTimer;
    [SerializeField]
    private float weaponDamage;
    [SerializeField]
    private Vector3 weaponShootPos;
    [SerializeField]
    private float weaponShootSpeed;


    // Start is called before the first frame update
    void Start()
    {
        weaponCooldownTimer = 0;
        DefaultSetting();
    }

    void DefaultSetting() {
        weaponCooldownTime = 3.0f;
        weaponShootSpeed = 20;
        weaponDamage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (FPSCameraShift.instance.startShift) {return;}
        if (weaponCooldownTimer >= weaponCooldownTime) {
            weaponCooldownTimer = 0;

            shootSound.Play();
            PlayerObjectsPooler.instance.SpawnFromPool("HomingProjectile", transform.position, Quaternion.LookRotation(PlayerMouseHandler.instance.GetMouseDir(), transform.up))
            .GetComponent<PlayerProjectile>().Shoot(PlayerMouseHandler.instance.GetMouseDir(),
                                                    weaponShootSpeed,
                                                    PlayerStats.instance.GetDamage() + weaponDamage);
        } else {
            weaponCooldownTimer += Time.deltaTime;
        }
    }
}
