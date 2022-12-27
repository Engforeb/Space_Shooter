using System.Collections.Generic;
using Infrastructure.Factory;
using UnityEngine;

public class BulletPool : IPool
{
    private readonly IGameFactory _gameFactory;
    private readonly Transform _parent;
    private readonly int _capacity;

    public BulletPool(IGameFactory gameFactory, Transform parent, int capacity)
    {
        _gameFactory = gameFactory;
        _parent = parent;
        _capacity = capacity;
        Generate();
    }

    private readonly List<GameObject> _ammoBatch = new ();

    public void Generate()
    {
        for (int i = 0; i < _capacity; i++)
        {
            Add();
        }
    }

    public GameObject Request()
    {
        foreach (var ammo in _ammoBatch)
        {
            if (ammo != null)
            {
                if (ammo.activeInHierarchy == false)
                {
                    ammo.SetActive(true);
                    ammo.transform.SetParent(_parent.transform);
                    return ammo;
                }
            }
            
        }
        return Add();
    }
    public GameObject Add()
    {
        GameObject ammo = _gameFactory.CreateBullet();
        ammo.transform.SetParent(_parent.transform);
        ammo.SetActive(false);
        _ammoBatch.Add(ammo);
        return ammo;
    }
}