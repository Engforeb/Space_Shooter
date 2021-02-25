using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Sound Manager is NULL.");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _playerHit;
    [SerializeField] private AudioClip _smallExplosion;
    [SerializeField] private AudioClip _playerExplosion;

    public void HitSound()
    {
        _audioSource.clip = _playerHit;
        _audioSource.Play();
    }

    public void SmallExplosion()
    {
        _audioSource.clip = _smallExplosion;
        _audioSource.Play();
    }

    public void PlayerExplosion()
    {
        _audioSource.clip = _playerExplosion;
        _audioSource.Play();
    }

}
