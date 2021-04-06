using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    private WaitForSeconds _secondsBeforeDestroy, _tinyPause;
    [SerializeField] private float _waitTimeBeforeDeactivate = 2f;
    public Transform _bulletPool;

    private bool _isTargetHit;

    private void Awake()
    {
        _bulletPool = FindObjectOfType<BulletPool>().transform;
        _isTargetHit = false;
    }
    private void OnEnable()
    {
       
        transform.parent = null;
        _isTargetHit = false;
    }

    private void Start()
    {
        _secondsBeforeDestroy = new WaitForSeconds(_waitTimeBeforeDeactivate);
        _tinyPause = new WaitForSeconds(0.01f);
    }
    private void Update()
    {
        if (_isTargetHit == false)
        {
            transform.position += transform.up * Time.deltaTime * _bulletSpeed;
            StartCoroutine(WaitAndDeactivate(gameObject));
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
            transform.SetParent(collision.transform);
            
            GameObject explosion = BulletExplosionPool.Instance.ExplosionRequest();
            explosion.transform.position = transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            
            if (gameObject.activeInHierarchy == true)
            {
                StartCoroutine(DisactivateAfterTinyPause());
            }
        }
    }

    private IEnumerator WaitAndDeactivate(GameObject bullet)
    {
        yield return _secondsBeforeDestroy;
        //_anim.SetTrigger("Off");
        this.gameObject.transform.SetParent(_bulletPool);
        this.gameObject.SetActive(false);
        
    }

    private IEnumerator DisactivateAfterTinyPause()
    {
        yield return _tinyPause;
        //_anim.SetTrigger("Off");
        this.gameObject.transform.SetParent(_bulletPool);
        gameObject.SetActive(false);
    }

    
}
