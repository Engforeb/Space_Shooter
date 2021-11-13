using System.Collections;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class MachineGun : MonoBehaviour, IWeapon
    {
        public float FireRate => fireRate;
        public IMagazine Magazine { get; private set; }
    
        public void Shoot()
        {
            StartCoroutine(ContinuousShoot());
        }

        [SerializeField] private float fireRate;
        [SerializeField] private BulletMagazine bulletMagazine;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private AudioClip shotSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform socket;

        private WaitForSeconds _fireRateYield;
        
        private void Awake()
        {
            if (bulletMagazine.GetComponent<IMagazine>() != null)
            {
                Magazine = bulletMagazine.GetComponent<IMagazine>();
            }
            
            _fireRateYield = new WaitForSeconds(fireRate);
            audioSource.clip = shotSound;
        }

        private IEnumerator ContinuousShoot()
        {
            while (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl))
            {
                muzzleFlash.Play();
                audioSource.Play();

                var bullet = Magazine.Release().Body;
                var socketTransform = socket.transform;
                bullet.transform.position = socketTransform.position;
                bullet.transform.rotation = socketTransform.rotation;

                yield return _fireRateYield;
            }
        }
    }
}
