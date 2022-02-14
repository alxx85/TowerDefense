using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected HeroLayer _type;
    [SerializeField] protected Ammo _ammoTemplate;

    public HeroLayer WeaponType => _type;

    public abstract void Shoot(Vector2 playerPosition, Transform target, int damageIncrease);

    protected int DamageIncrease(int percent)
    {
        if (percent > 0)
        {
            float increaseDamage = _damage - (float)_damage * percent / 100f;
            return (int)increaseDamage;
        }
        return 0;
    }
}
