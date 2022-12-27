using System.Collections;
using Infrastructure.Services;
using Interfaces;
using UnityEngine;

public class Shooter : MonoBehaviour, IShootable
{
    [SerializeField] private Transform socket;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem muzzleFlashParticles;

    [Range(0, 1)] [SerializeField] private float fireRate;

    private WaitForSeconds _fireRateYield;

    private IAmmo _ammo;
    private IPool _pool;

    private void Start()
    {
        _fireRateYield = new WaitForSeconds(fireRate);
        _pool = AllServices.Container.Single<IPool>();
    }

    private void OnEnable()
    {
        muzzleFlashParticles.Stop();
    }

    public void Shoot()
    {
        StartCoroutine(ContinuousShoot());
    }

    private IEnumerator ContinuousShoot()
    {
        while (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl))
        {
            muzzleFlashParticles.Play();
            audioSource.Play();

            GameObject bullet = _pool.Request();

            var myTransform = socket.transform;
            
            bullet.transform.position = myTransform.position;
            bullet.transform.rotation = myTransform.rotation;

            yield return _fireRateYield;
        }
    }
}
