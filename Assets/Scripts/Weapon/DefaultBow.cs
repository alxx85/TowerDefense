using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBow : Weapon
{
    public override void Shoot(Vector2 playerPosition, Transform target, int damageIncrease)
    {
        Ammo ammo = Instantiate(_ammoTemplate, playerPosition, Quaternion.identity);
        ammo.Init(target, _damage);
    }

}
