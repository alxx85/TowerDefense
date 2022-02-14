using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ammo
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    public override void Init(Transform target, int damage)
    {
        _damage = damage;
        RotateByDirection(target.GetComponent<CapsuleCollider2D>().bounds.center);
        Destroy(gameObject, _timeToDestroy);
    }

    private void RotateByDirection(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Debug.Log(enemy.name + "получил урон");
            //Destroy(gameObject);
        }
    }
}
