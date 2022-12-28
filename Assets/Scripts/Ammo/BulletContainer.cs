using UnityEngine;
namespace Ammo
{
    public class BulletContainer : MonoBehaviour
    {
        public int Capacity => capacity;
    
        [SerializeField] private int capacity;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}