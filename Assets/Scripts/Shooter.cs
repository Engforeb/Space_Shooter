﻿using System.Collections;
using Interfaces;
using UnityEngine;

public class Shooter : MonoBehaviour, IShootable
{
    [SerializeField] private Transform _socket;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private ParticleSystem _muzzleFlashParticles;

    [Range(0, 1)] [SerializeField] private float _fireRate;

    private WaitForSeconds _fireRateYield;

    private IAmmo _ammo;

    private void Start()
    {
        _fireRateYield = new WaitForSeconds(_fireRate);
    }

    private void OnEnable()
    {
        _muzzleFlashParticles.Stop();
    }

    public void Shoot()
    {
        StartCoroutine(ContinuousShoot());
    }

    private IEnumerator ContinuousShoot()
    {
        while (Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl))
        {
            _muzzleFlashParticles.Play();
            _audio.Play();

            GameObject bullet = AmmoPool.Instance.AmmoRequest();
            bullet.transform.position = _socket.transform.position;
            bullet.transform.rotation = _socket.transform.rotation;

            yield return _fireRateYield;
        }
    }
}