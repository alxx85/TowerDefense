using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected Layer _type;
    [SerializeField] protected Ammo _ammoTemplate;

    public Layer WeaponType => _type;

    public abstract void Shoot(Vector2 playerPosition, Transform target, int damageIncrease);
}
