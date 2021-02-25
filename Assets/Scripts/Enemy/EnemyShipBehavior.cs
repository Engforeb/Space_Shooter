using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyShipBehavior : MonoBehaviour, IDamageable
{
    public int Health { get ; set; }
    [SerializeField] private int _currentHealth; 
    private float _startHealth;

    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private EnemyScriptableObject _speed;
    [SerializeField] private EnemyScriptableObject _woundedValue; 
    [SerializeField] private EnemyScriptableObject _damagedValue;
    [SerializeField] private RectTransform _healthBar;
    private Slider _healthSlider;
    public bool isInStartPosition { get; set; }

    private void Start()
    {
        _startHealth = (float)_currentHealth;
        Health = _currentHealth;
        _healthSlider = _healthBar.GetComponent<Slider>();
        _healthSlider.value = (float)_currentHealth / _startHealth;


        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isInStartPosition)
        {
            transform.position += transform.up * Time.deltaTime * _speed.speed;
        }
            
    }
    public void Damage(int damageAmount)
    {
        if (Health >= 1)
        {
            SoundManager.Instance.HitSound();
        }
        else
        {
            return;
        }
        
        Health -= damageAmount;
        _currentHealth = Health;
        _healthSlider.value = (float)_currentHealth / _startHealth;
        

        if (Health < _woundedValue.wounded)
        {
            _anim.SetTrigger("Wounded");
        }

        if (Health < _damagedValue.damaged)
        {
            _anim.SetTrigger("Damaged");
        }

        if (Health < 1)
        {
            _anim.SetTrigger("Explosion");
            SoundManager.Instance.SmallExplosion();
            Destroy(gameObject, 0.8f);
        }
    }
}
