using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyShipBehavior : MonoBehaviour, IDamageable
{
    public bool isInStartPosition { get; set; }

    [SerializeField] private int _currentHealth;
    [SerializeField] private Animator _anim;
    [SerializeField] private EnemyScriptableObject _shipData;
    [SerializeField] private RectTransform _healthBar;

    private float _startHealth;
    private float _speed;
    private int _woundedValue; 
    private int _damagedValue;
    
    private Slider _healthSlider;

    public delegate void DestroyMe();
    public static event DestroyMe OnDestroy;

    private bool dead;

    private void Start()
    {
        _speed = _shipData.speed;
        _woundedValue = _shipData.wounded;
        _damagedValue = _shipData.damaged;
        _startHealth = (float)_currentHealth;
        
        _healthSlider = _healthBar.GetComponent<Slider>();
        _healthSlider.value = (float)_currentHealth / _startHealth;

        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInStartPosition)
        {
            transform.position += transform.up * Time.deltaTime * _speed;
        }
            
    }
    public void Damage(int damageAmount)
    {
        if (_currentHealth >= 1)
        {
            SoundManager.Instance.HitSound();
            _currentHealth -= damageAmount;
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
            

        _healthSlider.value = (float)_currentHealth / _startHealth;

        if (_currentHealth < _woundedValue)
        {
            _anim.SetTrigger("Wounded");
        }

        if (_currentHealth < _damagedValue)
        {
            _anim.SetTrigger("Damaged");
        }

        if (_currentHealth == 0 && !dead)
        {
            _anim.SetTrigger("Explosion");
            SoundManager.Instance.SmallExplosion();

            if (OnDestroy != null)
            {
                OnDestroy();
            }

            dead = true;
            Destroy(gameObject, 0.8f);
        }
    }
}
