﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public abstract class EnemyShipBehavior : MonoBehaviour, IDamageable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private EnemyScriptableObject shipData;
        [SerializeField] private RectTransform healthBar;
        [SerializeField] private GameObject shipExplosion, wounded, whiteSmoke;

        private float _startHealth;
        private int _woundedValue; 
        private int _damagedValue;
    
        private Slider _healthSlider;

        public static event Action OnDestroy;

        private bool _dead;
        private bool _woundedAnim, _smokeAnim;

        private void OnEnable()
        {
            _woundedAnim = false;
        }

        private void Start()
        {
            _woundedValue = shipData.wounded;
            _damagedValue = shipData.damaged;
            _startHealth = currentHealth;
        
            _healthSlider = healthBar.GetComponent<Slider>();
            _healthSlider.value = currentHealth / _startHealth;

            wounded.SetActive(false);
            whiteSmoke.SetActive(false);
        }
        public void Damage(int damageAmount)
        {   
            if (currentHealth >= 1)
            {
                currentHealth -= damageAmount;
            }
            else if (currentHealth < 0)
            {
                currentHealth = 0;
            }   

            _healthSlider.value = currentHealth / _startHealth;

            if (currentHealth < _woundedValue)
            {
                if (!_woundedAnim)
                {
                    wounded.gameObject.SetActive(true);
                    _woundedAnim = true;
                }
            }

            if (currentHealth < _damagedValue)
            {
                if (!_smokeAnim)
                {
                    whiteSmoke.gameObject.SetActive(true);
                    _smokeAnim = true;
                }
            }

            if (currentHealth != 0 || _dead) return;
            GameObject explosion = Instantiate(shipExplosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 1f);
            
            OnDestroy?.Invoke();

            _dead = true;
            Destroy(gameObject);
        }
    }
}