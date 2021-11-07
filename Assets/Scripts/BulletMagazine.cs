using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class BulletMagazine : MonoBehaviour, IMagazine
{
    public IAmmo Ammo { get; private set; }
    public int Capacity => capacity;
    
    public void FullRecharge()
    {
        for (var i = 0; i < capacity; i++)
        {
            _stackOfBullets.Push(Ammo);
        }
    }

    public void Recharge(int number)
    {
        for (var i = 0; i < number; i++)
        {
            _stackOfBullets.Push(Ammo);
        }
    }

    public IAmmo Release()
    {
        if (_stackOfBullets.Count > 0)
        {
            return _stackOfBullets.Pop();
        }

        return null;
    }

    [SerializeField] private int capacity;
    [SerializeField] private GameObject bulletPrefab;

    private readonly Stack<IAmmo> _stackOfBullets = new Stack<IAmmo>();

    private void Awake()
    {
        if (bulletPrefab.GetComponent<IAmmo>() != null)
        {
            Ammo = bulletPrefab.GetComponent<IAmmo>();
        }
        
        FullRecharge();
    }
}
