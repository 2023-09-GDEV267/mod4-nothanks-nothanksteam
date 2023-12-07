using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    public Slider musicSlider;
    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip scoringClip;
    public float fadeTime;

    [Header("SFX")]
    public Slider sfxSlider;
    public AudioClip cardFlip;
    public AudioClip cardShuffle;
    public AudioClip tokenClink;
    public AudioClip correctSound;
    public AudioClip errorSound;
    public float randomPitchMin;
    public float randomPitchMax;

    [Header("Set dynamically")]
    public static AudioManager S;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioClip currentClip;
    private bool fadeIn;
    private bool fadeOut;
    private float musicVol;
    private float sfxVol;
    private Toggle musicMute;
    private Toggle sfxMute;

    void Awake()
    {
        if (S == null) { S = this; }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }
        musicSource = gameObject.transform.Find("MusicManager").gameObject.GetComponent<AudioSource>();
        musicSource.volume = 0f;
        musicSource.clip = menuClip;
        sfxSource = gameObject.transform.Find("SFXManager").gameObject.GetComponent<AudioSource>();
        sfxSource.volume = sfxVol;
        fadeIn = true;
        fadeOut = false;
        currentClip = menuClip;
        musicSource.Play();
        musicMute = GameObject.Find("MuteMusic").GetComponent<Toggle>();
        sfxMute = GameObject.Find("MuteSFX").GetComponent<Toggle>();
        musicSlider.value = musicVol = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = sfxVol = PlayerPrefs.GetFloat("sfxVolume");
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

        musicVol = PlayerPrefs.GetFloat("musicVolume");
        sfxVol = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void ChangeVolume()
    {
        musicSource.volume = musicVol = musicSlider.value;
        sfxSource.volume = sfxVol = sfxSlider.value;
        
        PlayerPrefs.SetFloat("musicVolume", musicVol);
        PlayerPrefs.SetFloat("sfxVolume", sfxVol);
    }

    public void MuteMusic()
    {
        musicSource.gameObject.SetActive(!musicMute.isOn);
        if (musicSource.gameObject.activeSelf)
        {
            musicSource.Play();
        }
    }

    public void MuteSFX()
    {
        sfxSource.gameObject.SetActive(!sfxMute.isOn);
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
        sfxSource.pitch = 1f;
        sfxSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        sfxSource.PlayOneShot(cardFlip);
    }

    public void ShuffleSound()
    {
        sfxSource.pitch = 1f;
        sfxSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        sfxSource.PlayOneShot(cardShuffle);
    }

    public void PlaceToken()
    {
        sfxSource.pitch = 1f;
        sfxSource.pitch = Random.Range(randomPitchMin, randomPitchMax);
        sfxSource.PlayOneShot(tokenClink);
    }

    public void CollectTokensSound(int numOfTokens)
    {
        for (float i = 0; i < (numOfTokens * 0.1); i += 0.1f)
        {
            Invoke("PlaceToken", i);
        }
    }

    public void CorrectSound()
    {
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(correctSound);
    }

    public void ErrorSound()
    {
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(errorSound);
    }
}
