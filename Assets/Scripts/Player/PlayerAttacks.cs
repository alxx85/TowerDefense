using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerOptions))]
public class PlayerAttacks : MonoBehaviour
{
    [SerializeField, Range(2.5f, 5.0f)] private float _targetingRange = 2.7f;
    [SerializeField] private float _delay;

    private PlayerOptions _options;
    private Animator _animator;
    private Layer _layer = Layer.WoodenBow;
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
        _options = GetComponent<PlayerOptions>();
        _animator = GetComponent<Animator>();
        _options.SwingSpeedIncreaseChanged += OnSwingSpeedIncreaseChanged;
        _options.WeaponChanged += OnWeaponChanged;
        _timeBeforeAttack = _delay;
        _animator.SetLayerWeight((int)_layer, MaxWeight);
    }

    private void OnDisable()
    {
        _options.SwingSpeedIncreaseChanged -= OnSwingSpeedIncreaseChanged;
        _options.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        _timeAfterLastAttack += Time.deltaTime;

        if (_targetEnemy == null)
        {
            IsAcquireTarget();
        }
        else
        {
            float distance = Vector2.Distance(_targetEnemy.transform.position, transform.position);
            

            if (_timeBeforeAttack <= _timeAfterLastAttack)
            {
                AttackDirection();
                _timeAfterLastAttack = 0f;
            }

            if (distance > _targetingRange )
                _targetEnemy = null;
        }

    }

    private bool IsAcquireTarget()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(_targetingRange, _targetingRange), 0);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out Enemy enemy))
            {
                _targetEnemy = enemy;
                return true;
            }
        }
        return false;
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

    private void OnSwingSpeedIncreaseChanged(int percent)
    {
        _timeBeforeAttack = _delay - (_delay * percent) / 100;
    }

    private void OnWeaponChanged(Layer layer)
    {
        _animator.SetLayerWeight((int)_layer, MinWeight);
        _animator.SetLayerWeight((int)layer, MaxWeight);
        _layer = layer;
    }
}

public enum Layer
{
    Default,
    WoodenBow,
    CompositeBow,
    LegendaryBow,
    WoodenStaff,
    LegendaryStaff
}
