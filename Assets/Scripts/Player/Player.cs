using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _megaExplosion;
    [SerializeField] private int _health;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public int Health => _health;

    public static Action OnDamage;
    private bool _explosionStarted;

    private void OnEnable()
    {
        _explosionStarted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Damage(1);
            if (_health < 1)
            {
                StartCoroutine(_cameraShake.Shake(1, 0.1f));
                if (!_explosionStarted)
                {
                    _explosionStarted = true;
                    Instantiate(_megaExplosion, transform.position, Quaternion.identity);
                    SoundManager.Instance.PlayerExplosion();
                    _spriteRenderer.enabled = false;
                    Destroy(this.gameObject, 2f);
                }
            }   
        }
    }

    public void Damage(int amount)
    {
        _health -= amount;
        StartCoroutine(_cameraShake.Shake(0.2f, 0.05f));
        OnDamage?.Invoke();
    }
}
