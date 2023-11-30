using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip scoringClip;
    public float fadeTime;

    [Header("SFX")]
    public AudioClip cardFlip;
    public AudioClip tokenClink;
    public float randomPitchMin;
    public float randomPitchMax;
    private AudioSource sfxSource;

    private AudioSource musicSource;
    private AudioClip currentClip;
    private bool fadeIn;
    private bool fadeOut;
    private float musicVol;
    private float sfxVol;

    void Awake()
    {
        musicVol = PlayerPrefs.GetFloat("musicVolume");
        sfxVol = PlayerPrefs.GetFloat("SoundEffectsVolume");
        musicSource = gameObject.transform.Find("MusicManager").gameObject.GetComponent<AudioSource>();
        musicSource.volume = 0f;
        musicSource.clip = menuClip;
        sfxSource = gameObject.transform.Find("SFXManager").gameObject.GetComponent<AudioSource>();
        sfxSource.volume = sfxVol;
        fadeIn = true;
        fadeOut = false;
        currentClip = menuClip;
        musicSource.Play();

        //remove once player prefs are set
        musicVol = 1;
        sfxVol = 1;
    }

    void Update()
    {
        if (fadeIn && musicSource.volume < 1)
        {
            musicSource.volume += musicVol * Time.deltaTime / fadeTime;
            if (musicSource.volume >= musicVol)
            {
                musicSource.volume = musicVol;
                fadeIn = false;
            }
        }

        if (fadeOut && musicSource.volume > 0)
        {
            musicSource.volume -= musicVol * Time.deltaTime / fadeTime;
            if (musicSource.volume <= 0)
            {
                musicSource.volume = 0;
                musicSource.Stop();
                musicSource.clip = currentClip;
                musicSource.Play();
                fadeOut = false;
                fadeIn = true;
            }
        }
    }

    public void MenuMusic()
    {
        currentClip = menuClip;
        fadeOut = true;
    }

    public void GameMusic()
    {
        currentClip = gameClip;
        fadeOut = true;
    }

    public void ScoringMusic()
    {
        currentClip = scoringClip;
        fadeOut = true;
    }

    public void FlipCardSound()
    {
        sfxSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        sfxSource.PlayOneShot(cardFlip);
        sfxSource.pitch = 1f;
    }

    public void ShuffleSound()
    {
        for (float i = 0; i < 1; i += 0.02f)
        {
            Invoke("FlipCardSound", i);
        }
    }

    public void PlaceToken()
    {
        sfxSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        sfxSource.PlayOneShot(tokenClink);
        sfxSource.pitch = 1f;
    }

    public void CollectTokensSound(int numOfTokens)
    {
        for (float i = 0; i < (numOfTokens * 0.05); i += 0.05f)
        {
            Invoke("PlaceToken", i);
        }
    }
}
