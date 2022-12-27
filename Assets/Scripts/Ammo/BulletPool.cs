using System.Collections.Generic;
using Infrastructure.Factory;
using UnityEngine;
namespace Ammo
{
    public class BulletPool : IPool
    {
        private readonly IGameFactory _gameFactory;
        private readonly BulletContainer _bulletContainer;
        private readonly int _capacity;

        public BulletPool(IGameFactory gameFactory, BulletContainer bulletContainer)
        {
            _gameFactory = gameFactory;
            _bulletContainer = bulletContainer;
            _capacity = _bulletContainer.Capacity;
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
                        ammo.transform.SetParent(_bulletContainer.transform);
                        return ammo;
                    }
                }
            
            }
            return Add();
        }
        public GameObject Add()
        {
            GameObject ammo = _gameFactory.CreateBullet();
            ammo.transform.SetParent(_bulletContainer.transform);
            ammo.SetActive(false);
            _ammoBatch.Add(ammo);
            return ammo;
        }
    }
}