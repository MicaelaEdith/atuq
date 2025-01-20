using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private AudioClip gameMusic;

    [SerializeField]
    private AudioClip starSound;
    [SerializeField]
    private AudioClip coinSound;
    [SerializeField]
    private AudioClip coinAnimationSound;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioClip winSound;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    public bool musicOn = true;

    private void Awake()
    {
        Instance = this;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void PlaySFX(string sfxType)
    {
        switch (sfxType)
        {
            case "star":
                sfxSource.PlayOneShot(starSound);
                break;
            case "coin_animation":
                sfxSource.PlayOneShot(coinSound);
                break;
            case "coin":
                sfxSource.PlayOneShot(coinSound);
                break;
            case "lose":
                sfxSource.PlayOneShot(loseSound);
                break;
            case "win":
                sfxSource.PlayOneShot(winSound);
                break;

        }
    }

    public void ToggleMusicMute()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFXMute()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
