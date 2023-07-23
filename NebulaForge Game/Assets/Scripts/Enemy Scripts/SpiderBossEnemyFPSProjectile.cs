using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossEnemyFPSProjectile : MonoBehaviour, IPooledObject
{
    [SerializeField]
    protected Vector3 dir;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float lifeTime;
    [SerializeField]
    protected float lifeTimer;
    [SerializeField]
    protected Vector3 velocity;
    [SerializeField]
    protected float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        maxHeight = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileUpdate();

        if (lifeTimer >= lifeTime) {
            lifeTimer = 0;
            Reset();
            gameObject.SetActive(false);
        } else {
            lifeTimer += Time.deltaTime;
        }
    }

    public float GetDamage() { return damage; }

    public void OnObjectSpawn() { lifeTimer = 0; }

    public void Shoot(Vector3 _dir, float _speed, float _damage) {
        dir = _dir;
        speed = _speed;
        damage = _damage;
        velocity = new Vector3(Random.Range(-50.0f, 50.0f), 100.0f, Random.Range(-50.0f, 50.0f));
    }

    public void ProjectileUpdate() {
        if (transform.position.y >= maxHeight) {
            velocity = PlayerStats.instance.transform.position - transform.position;
        }
        
        transform.position += velocity * speed * Time.deltaTime;
    }

    public void Reset() {
        gameObject.SetActive(false);
        
        dir = Vector3.zero;
        speed = 0;
        damage = 0;
    }
    
    void OnTriggerEnter(Collider o) {
        if (o.gameObject.tag == "Player") {
            PlayerStats.instance.LoseHealth(damage);
            Reset();
        }
    }
}
