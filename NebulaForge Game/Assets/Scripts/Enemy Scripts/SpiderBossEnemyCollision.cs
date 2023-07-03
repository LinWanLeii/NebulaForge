using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemyCollision : MonoBehaviour
{
    public GameObject spiderMainGO;
    private SpiderBossEnemy spiderScript;
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
        spiderMainGO.SetActive(false);
        PlayerStats.instance.GainExp(1000);
    }
}
