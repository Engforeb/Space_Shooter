using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BulletPool();
            }

            return _instance;
        }
    }

    private void Awake() 
    {
        _instance = this;
    }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletsInPool;
    [SerializeField] private GameObject _bulletContainer;
    private List<GameObject> _bullets = new List<GameObject>();

    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < _bulletsInPool; i++)
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
                    return bullet;
                }
            }
            
        }
        return AddBulletToPool();
    }
    private GameObject AddBulletToPool()
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.SetParent(_bulletContainer.transform);
        bullet.SetActive(false);
        _bullets.Add(bullet);
        return bullet;
    }
}
