using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    [SerializeField, Range(0.5f, 5f)] protected float _timeToDestroy = 1f;

    protected int _damage;

    public abstract void Init(Transform target, int damage);
}
