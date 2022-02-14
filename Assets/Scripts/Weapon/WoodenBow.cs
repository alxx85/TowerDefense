using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : Weapon
{
    public override void Shoot(Vector2 startPosition, Transform target, int damageIncrease)
    {
        Ammo ammo = Instantiate(_ammoTemplate, startPosition, Quaternion.identity);
        ammo.Init(target, _damage + damageIncrease);
    }
}
