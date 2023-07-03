using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicProjectile : PlayerProjectile, IPooledObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Shoot(Vector3 _dir, float _speed, float _damage) {
        dir = _dir;
        speed = _speed;
        damage = _damage;
    }

    public override void ProjectileUpdate() {
        transform.position += dir * speed * Time.deltaTime;
    }

    public override void Reset() {
        gameObject.SetActive(false);
        
        dir = Vector3.zero;
        speed = 0;
        damage = 0;
    }
}
