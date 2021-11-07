using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Bullets : MonoBehaviour, IMagazine
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

    public IAmmo Recharge(int number)
    {
        if (_stackOfBullets.Count != 0)
        {
            return _stackOfBullets.Pop();            
        }
        else
        {
            Debug.Log("Out of bullets");
        }

        return null;
    }

    [SerializeField] private int capacity;
    [SerializeField] private GameObject bulletPrefab;

    private Stack<IAmmo> _stackOfBullets = new Stack<IAmmo>();

    private void Start()
    {
        if (bulletPrefab.GetComponent<IAmmo>() != null)
        {
            Ammo = bulletPrefab.GetComponent<IAmmo>();
        }
        
        FullRecharge();
    }
}
