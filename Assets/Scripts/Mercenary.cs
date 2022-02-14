using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mercenary : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2.0f)] private float _targetingRange = 0.5f;
    [SerializeField] private float _attackDelay;
    [SerializeField] private MercenaryLayer _layerDirection = MercenaryLayer.Down;
    [SerializeField] private Weapon _weapon;

    private PlayerAttacks _player;
    private Animator _animator;
    private Enemy _targetEnemy;
    private Vector2 _directionOffset = Vector2.down;
    private float _timeAfterLastAttack;
    private bool _isPlaced = true;

    private const string Attack = "Attack";
    private const float MaxLayerWeight = 1f;
    private const float MinLayerWeight = 0;

    public bool IsPlaced => _isPlaced;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetLayerWeight((int)_layerDirection, MaxLayerWeight);
    }

    private void Update()
    {
        if (_isPlaced)
        {
            Vector2 position = transform.position;
            _timeAfterLastAttack += Time.deltaTime;

            if (_targetEnemy == null)
            {
                position += _directionOffset;
                _targetEnemy = _player.IsAcquireTarget(position, _targetingRange);
            }
            else
            {
                float distance = Vector2.Distance(_targetEnemy.transform.position, transform.position);

                if (_attackDelay <= _timeAfterLastAttack)
                {
                    _animator.SetTrigger(Attack);
                    _weapon.Shoot(transform.position, _targetEnemy.transform, 0);
                    _timeAfterLastAttack = 0;
                }

                if (distance > _targetingRange)
                    _targetEnemy = null;
            }
        }
    }

    public void Init(PlayerAttacks player)
    {
        _player = player;
    }

    public void SetDirection(MercenaryLayer direction)
    {
        if (_isPlaced)
        {
            _animator.SetLayerWeight((int)_layerDirection, MinLayerWeight);
            _animator.SetLayerWeight((int)direction, MaxLayerWeight);
            _layerDirection = direction;
            
            switch (direction)
            {
                case MercenaryLayer.Base:
                    _directionOffset = Vector2.zero;
                    break;
                case MercenaryLayer.Down:
                    _directionOffset = Vector2.down;
                    break;
                case MercenaryLayer.Up:
                    _directionOffset = Vector2.up;
                    break;
                case MercenaryLayer.Left:
                    _directionOffset = Vector2.left;
                    break;
                case MercenaryLayer.Right:
                    _directionOffset = Vector2.right;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 position = transform.position;
        position.x += _directionOffset.x;
        position.y += _directionOffset.y;
        position.z += 0.01f;

        Gizmos.DrawWireCube(position, new Vector3(_targetingRange, _targetingRange));
    }
}

public enum MercenaryLayer
{
    Base,
    Down,
    Up,
    Left,
    Right
}