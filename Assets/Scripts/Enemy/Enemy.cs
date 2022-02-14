using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    private Path _path;
    private Vector3 _checkPointPosition;
    private Vector3 _offset = Vector3.zero;
    private int _currentCheckPointNumber = 0;
    private bool _isAlive = true;

    public event UnityAction<Enemy> Dying;
    public event UnityAction<Vector3> CheckPointPositionChanged;

    public bool IsAlive => _isAlive;

    private void Update()
    {
        if (_isAlive)
        {
            if (transform.position - _offset == _checkPointPosition)
            {
                _checkPointPosition = _path.GetNextPoint(++_currentCheckPointNumber);
                if (_checkPointPosition != default)
                    CheckPointPositionChanged?.Invoke(_checkPointPosition + _offset);
            }
        }
        else
            Destroy(gameObject, 5f);
    }

    public void Init(Path currentPath, float offset)
    {
        _path = currentPath;
        _offset.x = Random.Range(-offset, offset);
        _offset.y = Random.Range(-offset, offset);
        _checkPointPosition = _path.StartPoint();
        CheckPointPositionChanged?.Invoke(_checkPointPosition + _offset);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Dying?.Invoke(this);
            _isAlive = false;
        }
    }

    public void EnterEndPoint()
    {
        Destroy(gameObject);
    }
}
