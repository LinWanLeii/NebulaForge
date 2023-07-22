using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemyCollision : MonoBehaviour
{
    public GameObject spiderMainGO;
    private SpiderBossEnemy spiderScript;
    public SpiderBossEnemyAttack spiderAttackScript;
    void Start()
    {
        spiderScript = spiderMainGO.GetComponent<SpiderBossEnemy>();
    }

    void OnTriggerEnter(Collider o) {
        if (o.gameObject.tag == "PlayerProjectile") {
            PlayerProjectile p = o.GetComponent<PlayerProjectile>();
            spiderScript.currHealth -= p.GetDamage();

            if (spiderScript.currHealth <= 0) {
                Die();
            }

            p.Reset();
        }
    }

    void Die() {
        if (FPSCamera.instance.isFPS) {
            spiderMainGO.SetActive(false);
            //PlayerStats.instance.GainExp(1000);
            LevelManager.instance.Victory();
        } else {
            spiderScript.currHealth = spiderScript.maxHealth;
            FPSCameraShift.instance.StartShift();
            spiderAttackScript.PutOnHold(true);
        }

    }
}
