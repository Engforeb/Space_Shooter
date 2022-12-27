using System.Threading.Tasks;
using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour, IAmmo
{
    public GameObject Body => gameObject;
    public float Speed => bulletSpeed;
    public float Lifetime => lifetime;
    public int Damage => damage;
    
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifetime;
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosion;
    [SerializeField] private SpriteRenderer spriteRenderer;

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

    private void Update()
    {
        if (_targetHit == false)
        {
            Move();
        }
    }

    public void Move()
    {
        var transform1 = transform;
        transform1.position += transform1.up * (Time.deltaTime * bulletSpeed);
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        _targetHit = true;

        IDamageable obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.Damage(damage);
            var position = transform.position;
            position = collision.ClosestPoint(position);
            transform.position = position;

            spriteRenderer.enabled = false;
            
            explosion.SetActive(true);
            explosion.transform.position = position;
            explosion.GetComponent<ParticleSystem>().Play();

            await Task.Delay(1000);

            gameObject.SetActive(false);
            
            spriteRenderer.enabled = true;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
