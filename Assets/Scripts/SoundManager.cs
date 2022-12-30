using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;
    private void Awake() => _instance = this;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip smallExplosion;
    [SerializeField] private AudioClip playerExplosion;

    public void HitSound()
    {
        audioSource.clip = playerHit;
        audioSource.Play();
    }

    public void SmallExplosion()
    {
        audioSource.clip = smallExplosion;
        audioSource.Play();
    }

    public void PlayerExplosion()
    {
        audioSource.clip = playerExplosion;
        audioSource.Play();
    }
}
