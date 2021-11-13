using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionPool : MonoBehaviour
{
    private static BulletExplosionPool _instance;
    public static BulletExplosionPool Instance => _instance;

    private void Awake() 
    {
        _instance = this;
    }

    [SerializeField] private GameObject _bulletExplosionPrefab;
    [SerializeField] private int _explosionsInPool;
    [SerializeField] private GameObject _explosionsContainer;
    private List<GameObject> _explosions = new List<GameObject>();

    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < _explosionsInPool; i++)
        {
            AddBulletToPool();
        }
    }

    public GameObject ExplosionRequest()
    {
        foreach (var explosion in _explosions)
        {
            if (explosion != null)
            {
                if (explosion.activeInHierarchy == false)
                {
                    explosion.SetActive(true);
                    return explosion;
                }
            }
            
        }
        return AddBulletToPool();
    }
    private GameObject AddBulletToPool()
    {
        GameObject explosion = Instantiate(_bulletExplosionPrefab);
        explosion.transform.SetParent(_explosionsContainer.transform);
        explosion.SetActive(false);
        _explosions.Add(explosion);
        return explosion;
    }
}
