using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletsInPool;
    private readonly List<GameObject> _bullets = new List<GameObject>();

    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < bulletsInPool; i++)
        {
            AddBulletToPool();
        }
    }

    public GameObject BulletRequest()
    {
        foreach (var bullet in _bullets)
        {
            if (bullet != null)
            {
                if (bullet.activeInHierarchy == false)
                {
                    bullet.SetActive(true);
                    bullet.transform.SetParent(gameObject.transform);
                    return bullet;
                }
            }
            
        }
        return AddBulletToPool();
    }
    private GameObject AddBulletToPool()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.SetParent(gameObject.transform);
        bullet.SetActive(false);
        _bullets.Add(bullet);
        return bullet;
    }
}
