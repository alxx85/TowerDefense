using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttacks : MonoBehaviour
{
    [SerializeField, Range(2.5f, 5.0f)] private float _targetingRange = 2.7f;
    [SerializeField] private float _delay;

    private Animator _animator;
    private HeroLayer _layer = HeroLayer.WoodenBow;
    private PlayerOptions _options;
    private float _timeAfterLastAttack;
    private float _timeBeforeAttack;
    private Enemy _targetEnemy;

    private const string Left = "Left";
    private const string Right = "Right";
    private const string Up = "Up";
    private const string Down = "Down";
    private const float MaxWeight = 1;
    private const float MinWeight = 0;

    private void Awake()
    {
        _options = Game.Instance.GetComponent<PlayerOptions>();
        _animator = GetComponent<Animator>();
        _options.WeaponChanged += OnWeaponChanged;
        _timeBeforeAttack = _delay - _delay * _options.SwingSpeedIncreasePercent / 100;
        _animator.SetLayerWeight((int)_layer, MaxWeight);
    }

    private void OnDisable()
    {
        _options.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        _timeAfterLastAttack += Time.deltaTime;

        if (_targetEnemy == null)
        {
            _targetEnemy = Game.Instance.IsAcquireTarget(transform.position, _targetingRange);
        }
        else
        {
            if (_timeBeforeAttack <= _timeAfterLastAttack)
            {
                AttackDirection();
                _timeAfterLastAttack = 0f;
            }

            _targetEnemy = Game.Instance.DidLoseTarget(transform.position, _targetEnemy, _targetingRange);
        }
    }

    private void AttackDirection()
    {
        Vector2 direction = transform.position - _targetEnemy.transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;

        switch (direction)
        {
            case Vector2 vector when vector.x > 0:
                _animator.SetTrigger(Left);
                break;
            case Vector2 vector when vector.x < 0:
                _animator.SetTrigger(Right);
                break;
            case Vector2 vector when vector.y > 0:
                _animator.SetTrigger(Down);
                break;
            case Vector2 vector when vector.y < 0:
                _animator.SetTrigger(Up);
                break;
        }
        Shoot(_targetEnemy);
    }

    private void Shoot(Enemy target)
    {
        _options.Weapon.Shoot(transform.position, target.transform, _options.DamageIncrease);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = transform.position;
        position.z += 0.01f;

        Gizmos.DrawWireCube(position, new Vector3(_targetingRange, _targetingRange));
    }

    private void OnWeaponChanged(HeroLayer layer)
    {
        _animator.SetLayerWeight((int)_layer, MinWeight);
        _animator.SetLayerWeight((int)layer, MaxWeight);
        _layer = layer;
    }
}
