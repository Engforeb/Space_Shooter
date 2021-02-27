using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Animator _anim;

    private bool _isTargetHit;

    private void OnEnable()
    {
        transform.parent = null;
    }
    private void Update()
    {
        if (_isTargetHit == false)
        {
            transform.position += transform.up * Time.deltaTime * _bulletSpeed;
            Destroy(gameObject, 5);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isTargetHit = true;
        
        IDamageable obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.Damage(1);
            transform.position = collision.ClosestPoint(transform.position);
            _anim.SetTrigger("Hit");
            Destroy(gameObject, 0.1f);
        }
    }
}
