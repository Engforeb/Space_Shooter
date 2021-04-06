using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyShipBehavior : MonoBehaviour, IDamageable
{
    private bool _isInStartPosition;

    [SerializeField] private int _currentHealth;
    [SerializeField] private Animator _anim;
    [SerializeField] private EnemyScriptableObject _shipData;
    [SerializeField] private RectTransform _healthBar;
    [SerializeField] private GameObject _hitExplosion;
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private GameObject _shipExplosion, _wounded, _whiteSmoke;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _startHealth;
    private float _speed;
    private int _woundedValue; 
    private int _damagedValue;
    
    private Slider _healthSlider;

    public delegate void DestroyMe();
    public static event DestroyMe OnDestroy;

    private bool _dead;
    private bool _woundedAnim, _smokeAnim;

    private Vector3 _parent;
    private Vector3 _halfwayToParent;
    private Vector3 _collisionPoint;

    private void OnEnable()
    {
        Spawner.OnAllInPlace += StartMoving;
        _woundedAnim = false;
    }

    private void OnDisable()
    {
        Spawner.OnAllInPlace -= StartMoving;
    }
    private void Start()
    {
        _speed = _shipData.speed;
        _woundedValue = _shipData.wounded;
        _damagedValue = _shipData.damaged;
        _startHealth = (float)_currentHealth;
        
        _healthSlider = _healthBar.GetComponent<Slider>();
        _healthSlider.value = (float)_currentHealth / _startHealth;

        _anim = GetComponent<Animator>();

        _wounded.SetActive(false);
        _whiteSmoke.SetActive(false);

        //_parent = transform.parent.transform.position;
        //_halfwayToParent = (_parent - this.transform.position) * 0.5f;

    }
    private void Update()
    {
        if (_isInStartPosition)
        {
            transform.position += transform.up * Time.deltaTime * _speed;
            //transform.position = Vector3.MoveTowards(transform.position, _halfwayToParent, 1f * Time.deltaTime);
        }
    }

    private void StartMoving()
    {
        _isInStartPosition = true;
    }
    public void Damage(int damageAmount)
    {
        
        
        if (_currentHealth >= 1)
        {
            //SoundManager.Instance.HitSound();
            //GameObject hit = Instantiate(_hitExplosion, _collisionPoint, Quaternion.identity);
            //hit.transform.SetParent(transform);
            _currentHealth -= damageAmount;
        }
        else if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
            

        _healthSlider.value = (float)_currentHealth / _startHealth;

        if (_currentHealth < _woundedValue)
        {

            _anim.enabled = false;
            _anim.enabled = false;

            if (!_woundedAnim)
            {
                //GameObject woundedAnim = Instantiate(_wounded);
                //woundedAnim.transform.SetParent(transform);
                //woundedAnim.transform.localPosition = new Vector3(0, -3, 0);
                //woundedAnim.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                _wounded.gameObject.SetActive(true);
                _woundedAnim = true;
            }
            
        }

        if (_currentHealth < _damagedValue)
        {
            if (!_smokeAnim)
            {
                //GameObject smoke = Instantiate(_whiteSmoke);
                //smoke.transform.SetParent(transform);
                //smoke.transform.localPosition = new Vector3(0, 1.3f, 0);
                //smoke.transform.localScale = new Vector3(1f, 1f, 1f);
                _whiteSmoke.gameObject.SetActive(true);
                _spriteRenderer.sortingOrder = -1;
                _smokeAnim = true;
            }

            

        }

        if (_currentHealth == 0 && !_dead)
        {
            //_anim.SetTrigger("Explosion");
            //SoundManager.Instance.SmallExplosion();
            GameObject explosion = Instantiate(_shipExplosion);
            explosion.transform.position = transform.position;
            Destroy(explosion, 1f);

            if (OnDestroy != null)
            {
                OnDestroy();
            }

            _dead = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            _collisionPoint = collision.collider.ClosestPoint(transform.position);
        }
    }

}
