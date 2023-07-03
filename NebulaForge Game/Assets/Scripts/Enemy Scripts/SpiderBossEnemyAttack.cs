using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemyAttack : MonoBehaviour
{
    public GameObject chargedAttackGO;
    public Material myMat;
    public float damage;
    public float alpha;
    public float chargeSpeed;
    public bool isChargingUp;
    public bool isDamaging;
    public float damageLingerTime;
    public float damageLingerTimer;
    public Color fullChargeColor;

    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().material;
        fullChargeColor = myMat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChargingUp) {
            if (alpha < 1.0f) {
                damageLingerTimer += Time.deltaTime;
                alpha += Time.deltaTime * chargeSpeed;
                myMat.color = new Color(fullChargeColor.r, fullChargeColor.g, fullChargeColor.b, alpha);
            } else {
                chargedAttackGO.SetActive(true);
            myMat.color = new Color(0, 0, 0, 0);
                isChargingUp = false;
                isDamaging = true;
            }
        } else if (isDamaging) {
            if (damageLingerTimer >= damageLingerTime) {
                damageLingerTimer = 0;
                isDamaging = false;
                chargedAttackGO.SetActive(false);
            } else {
                damageLingerTimer += Time.deltaTime;
            }
        } else {
            chargedAttackGO.SetActive(false);
            myMat.color = new Color(0, 0, 0, 0);
            
        }
    }

    public void StartCharge(float _chargeSpeed, float _damageLingerTime, float _damage) {
        isChargingUp = true;
        isDamaging = false;
        alpha = 0;
        chargeSpeed = _chargeSpeed;
        damageLingerTime = _damageLingerTime;
        damageLingerTimer = 0;
        damage = _damage;
    }

    void OnCollisionStay(Collision col) {
        if (isDamaging) {
            if (col.gameObject.tag == "Player") {
                PlayerStats.instance.LoseHealth(damage);
            }
        }
    }
}
