using Infrastructure.Services;
using UnityEngine;
namespace Ammo
{
    public interface IPool : IService
    {
        public void Generate();

        public GameObject Add();

        public GameObject Request();
    }
}
