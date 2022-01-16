using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip playerShotSound;
    public AudioClip asteriodExplosion;
    public AudioClip largeExplosion;
    public AudioClip enemyShotSound;
    public AudioClip rocketSound;
    public AudioClip bonusHpSound;
    public AudioClip bossFight;
    public AudioClip defeatSong;
    public AudioClip[] playlist;
    private int _musicIndex = 0;
    public AudioSource audioSource;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scène !");
            return;
        }
        Instance = this;
    }
    
    private void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }
    
    private void Update()
    {
        if (audioSource.isPlaying) return;
        PlayNextSong();
    }

    public void PlayNextSong()
    {
        _musicIndex = (_musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[_musicIndex];
        audioSource.Play();
    }

    public void PlayerShot()
    {
        MakeSound(playerShotSound);
    }
    public void AsteriodExplosion()
    {
        MakeSound(asteriodExplosion);
    }

    public void EnemyShot()
    {
        MakeSound(enemyShotSound);
    }
    
    public void RocketLaunchSound()
    {
        MakeSound(rocketSound);
    }
    public void RocketExplosionSound()
    {
        MakeSound(largeExplosion);
    }
    public void BonusHpPickUp()
    {
        MakeSound(bonusHpSound);
    }
    
    public void BossFight()
    {
        audioSource.clip = bossFight;
        audioSource.Play();
    }

    public void Defeat()
    {
        audioSource.clip = defeatSong;
        audioSource.Play();
    }

    private void MakeSound(AudioClip originalClip)
    {
        audioSource.PlayOneShot(originalClip);
    }
}
