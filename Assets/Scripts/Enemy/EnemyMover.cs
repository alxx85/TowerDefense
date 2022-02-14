using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Enemy _enemy;
    private Vector3 _pointPosition;

    private const string Left = "MoveLeft";
    private const string Right = "MoveRight";
    private const string Up = "MoveUp";
    private const string Down = "MoveDown";
    private const string Death = "Death";

    private void OnEnable()
    {
        _enemy.CheckPointPositionChanged += OnPointPositionChanged;
    }

    private void OnDisable()
    {
        _enemy.CheckPointPositionChanged -= OnPointPositionChanged;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_enemy.IsAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointPosition, _speed * Time.deltaTime);
        }
        else
        {
            _animator.Play(Death);
            Destroy(this);
        }
    }

    private void OnPointPositionChanged(Vector3 position)
    {
        _pointPosition = position;
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        Vector2 direction = new Vector2(transform.position.x - _pointPosition.x, transform.position.y - _pointPosition.y);
        switch (direction)
        {
            case Vector2 vector when vector.x < 0:
                _animator.Play(Right);
                break;
            case Vector2 vector when vector.x > 0:
                _animator.Play(Left);
                break;
            case Vector2 vector when vector.y < 0:
                _animator.Play(Up);
                break;
            case Vector2 vector when vector.y > 0:
                _animator.Play(Down);
                break;
        }
    }
}
