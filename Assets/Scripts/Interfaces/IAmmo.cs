using UnityEngine;
namespace Interfaces
{
    public interface IAmmo
    {
        GameObject Body { get; }
        float Speed { get; }
        float Lifetime { get; }
        int Damage { get; }

        void Move();
    }
}
