using System.Collections;
using Ammo;
using Infrastructure.Services;
using InputClasses;
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
    private IInput _iInput;
    private bool _shootStarted;

    public void Init()
    {
        muzzleFlashParticles.Stop();
        _fireRateYield = new WaitForSeconds(fireRate);
        _pool = AllServices.Container.Single<IPool>();
        _iInput = AllServices.Container.Single<IInput>();
    }

    public void Shoot()
    {
        if (_iInput.IsFire && !_shootStarted)
        {
            StartCoroutine(ContinuousShoot());
        }
        else if (!_iInput.IsFire && _shootStarted)
        {
            StopAllCoroutines();
            _shootStarted = false;
        }
    }

    private void Update()
    {
        _iInput.UserInput();
        Shoot();
    }

    private IEnumerator ContinuousShoot()
    {
        _shootStarted = true;
        
        while (true)
        {
            muzzleFlashParticles.Play();
            audioSource.Play();

            GameObject bullet = _pool.Request();

            Transform myTransform = socket.transform;
            
            bullet.transform.position = myTransform.position;
            bullet.transform.rotation = myTransform.rotation;

            yield return _fireRateYield;
        }
    }
}
