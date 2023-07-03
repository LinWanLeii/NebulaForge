using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour, IPooledObject
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

    // Start is called before the first frame update
    void Start()
    {
        
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

    public virtual void Shoot(Vector3 _dir, float _speed, float _damage) {}

    public virtual void ProjectileUpdate() {}

    public virtual void Reset() {}
}
