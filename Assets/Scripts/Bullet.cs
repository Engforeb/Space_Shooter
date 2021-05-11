using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform _bulletPool;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _waitTimeBeforeDeactivate = 2f;

    private WaitForSeconds _secondsBeforeDestroy;
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
    }
    private void Update()
    {
        if (_isTargetHit == false)
        {
            Move();
            StartCoroutine(WaitAndDeactivate(gameObject));
        }
    }

    private void Move()
    {
        transform.position += transform.up * Time.deltaTime * _bulletSpeed;
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

            gameObject.SetActive(false);
        }
    }
    private IEnumerator WaitAndDeactivate(GameObject bullet)
    {
        yield return _secondsBeforeDestroy;
        gameObject.transform.SetParent(_bulletPool);
        gameObject.SetActive(false);
    }
}
