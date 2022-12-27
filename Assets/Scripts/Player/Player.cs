using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable, IGetSizeable, IAnimatable
    {
        public float Width { get; private set; }
        public float Height { get; private set; }
        public int Health => health;
        public Animator Animator { get; private set; }
        
        public static Action OnDamage;
        
        [SerializeField] private GameObject megaExplosion;
        [SerializeField] private int health;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool _explosionStarted;
        private CameraShake _cameraShake;

        public void Init(CameraShake cameraShake)
        {
            Width = transform.GetComponent<SpriteRenderer>().bounds.size.x;
            Height = transform.GetComponent<SpriteRenderer>().bounds.size.y;
            Animator = GetComponent<Animator>();
            
            _explosionStarted = false;
            _cameraShake = cameraShake;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Damage(1);
                if (health < 1)
                {
                    StartCoroutine(_cameraShake.Shake(1, 0.1f));
                    if (!_explosionStarted)
                    {
                        _explosionStarted = true;
                        Instantiate(megaExplosion, transform.position, Quaternion.identity);
                        SoundManager.Instance.PlayerExplosion();
                        spriteRenderer.enabled = false;
                        Destroy(this.gameObject, 2f);
                    }
                }   
            }
        }

        public void Damage(int amount)
        {
            health -= amount;
            StartCoroutine(_cameraShake.Shake(0.2f, 0.05f));
            OnDamage?.Invoke();
        }
    }
}
