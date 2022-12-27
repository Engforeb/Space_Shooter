using System.Collections;
using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour, IAmmo
{
    public GameObject Body => gameObject;
    public float Speed => bulletSpeed;
    public float Lifetime => lifetime;
    public int Damage => damage;
    
    public Transform ammoPool;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifetime;
    [SerializeField] private int damage;
    
    private WaitForSeconds _secondsBeforeDestroy;
    private bool _targetHit;

    private void Awake()
    {
        _targetHit = false;
    }
    private void OnEnable()
    {  
        transform.parent = null;
        _targetHit = false;
    }

    private void Start()
    {
        _secondsBeforeDestroy = new WaitForSeconds(lifetime);
    }
    private void Update()
    {
        if (_targetHit == false)
        {
            Move();
            StartCoroutine(WaitAndDeactivate());
        }
    }

    public void Move()
    {
        var transform1 = transform;
        transform1.position += transform1.up * (Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _targetHit = true;

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
        gameObject.SetActive(false);
    }
}
