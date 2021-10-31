using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform bulletPool;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float waitTimeBeforeDeactivate = 2f;

    private WaitForSeconds _secondsBeforeDestroy;
    private bool _isTargetHit;

    private void Awake()
    {
        bulletPool = FindObjectOfType<BulletPool>().transform;
        _isTargetHit = false;
    }
    private void OnEnable()
    {  
        transform.parent = null;
        _isTargetHit = false;
    }

    private void Start()
    {
        _secondsBeforeDestroy = new WaitForSeconds(waitTimeBeforeDeactivate);
    }
    private void Update()
    {
        if (_isTargetHit == false)
        {
            Move();
            StartCoroutine(WaitAndDeactivate());
        }
    }

    private void Move()
    {
        var transform1 = transform;
        transform1.position += transform1.up * (Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isTargetHit = true;

        IDamageable obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.Damage(1);
            var position = transform.position;
            position = collision.ClosestPoint(position);
            transform.position = position;

            GameObject explosion = BulletExplosionPool.Instance.ExplosionRequest();
            explosion.transform.position = position;
            explosion.GetComponent<ParticleSystem>().Play();

            gameObject.SetActive(false);
        }
    }
    private IEnumerator WaitAndDeactivate()
    {
        yield return _secondsBeforeDestroy;
        gameObject.transform.SetParent(bulletPool);
        gameObject.SetActive(false);
    }
}
