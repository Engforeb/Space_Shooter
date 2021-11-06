using System;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    private static AmmoPool _instance;
    public static AmmoPool Instance => _instance;

    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } 
        else 
        {
            _instance = this;
        }
    }

    public enum AmmoTypes
    {
        Bullet,
        Rocket,
        Fire,
        Laser,
    }

    [SerializeField] private AmmoTypes ammoType = AmmoTypes.Bullet;
    
    [Header("Available ammo")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject laser;
    
    [SerializeField] private int capacity;
    
    private readonly List<GameObject> _ammoBatch = new List<GameObject>();

    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < capacity; i++)
        {
            AddAmmo();
        }
    }

    public GameObject AmmoRequest()
    {
        foreach (var ammo in _ammoBatch)
        {
            if (ammo != null)
            {
                if (ammo.activeInHierarchy == false)
                {
                    ammo.SetActive(true);
                    ammo.transform.SetParent(gameObject.transform);
                    return ammo;
                }
            }
            
        }
        return AddAmmo();
    }
    private GameObject AddAmmo()
    {
        GameObject ammo = GetAmmo();
        ammo.transform.SetParent(gameObject.transform);
        ammo.SetActive(false);
        _ammoBatch.Add(ammo);
        return bullet;
    }

    private GameObject GetAmmo()
    {
        GameObject ammo = null;
        switch (ammoType)
        {
            case AmmoTypes.Bullet:
                if (bullet != null)
                    ammo = Instantiate(bullet);
                break;
            case AmmoTypes.Fire:
                if (fire != null)
                    ammo = Instantiate(fire);
                break;
            case AmmoTypes.Laser:
                if (laser != null)
                    ammo = Instantiate(laser);
                break;
            case AmmoTypes.Rocket:
                if (rocket != null)
                    ammo = Instantiate(rocket);
                break;
        }
        return ammo;
    }
}
