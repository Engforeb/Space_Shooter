using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyShipBehavior : MonoBehaviour, IDamageable
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private EnemyScriptableObject _shipData;
    [SerializeField] private RectTransform _healthBar;
    [SerializeField] private GameObject _shipExplosion, _wounded, _whiteSmoke;

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
        _woundedValue = _shipData.wounded;
        _damagedValue = _shipData.damaged;
        _startHealth = (float)_currentHealth;
        
        _healthSlider = _healthBar.GetComponent<Slider>();
        _healthSlider.value = (float)_currentHealth / _startHealth;

        _wounded.SetActive(false);
        _whiteSmoke.SetActive(false);
    }
    public void Damage(int damageAmount)
    {   
        if (_currentHealth >= 1)
        {
            _currentHealth -= damageAmount;
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }   

        _healthSlider.value = _currentHealth / _startHealth;

        if (_currentHealth < _woundedValue)
        {
            if (!_woundedAnim)
            {
                _wounded.gameObject.SetActive(true);
                _woundedAnim = true;
            }
        }

        if (_currentHealth < _damagedValue)
        {
            if (!_smokeAnim)
            {
                _whiteSmoke.gameObject.SetActive(true);
                _smokeAnim = true;
            }
        }

        if (_currentHealth == 0 && !_dead)
        {
            GameObject explosion = Instantiate(_shipExplosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 1f);
            
            OnDestroy?.Invoke();

            _dead = true;
            Destroy(gameObject);
        }
    }
}
