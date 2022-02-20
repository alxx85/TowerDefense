using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _delay;

    public Sprite Image => _sprite;
    public float Delay => _delay;

    public abstract void UsingSkill();
}
