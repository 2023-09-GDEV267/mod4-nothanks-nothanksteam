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
    public AudioClip tokenClink;
    public AudioClip correctSound;
    public AudioClip errorSound;
    public float randomPitchMin;
    public float randomPitchMax;

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
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }
        musicSlider.value = musicVol = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = sfxVol = PlayerPrefs.GetFloat("sfxVolume");
        musicSource = gameObject.transform.Find("MusicManager").gameObject.GetComponent<AudioSource>();
        musicSource.volume = 0f;
        musicSource.clip = menuClip;
        sfxSource = gameObject.transform.Find("SFXManager").gameObject.GetComponent<AudioSource>();
        sfxSource.volume = sfxVol;
        musicMute = GameObject.Find("MuteMusic").GetComponent<Toggle>();
        sfxMute = GameObject.Find("MuteSFX").GetComponent<Toggle>();
        fadeIn = true;
        fadeOut = false;
        currentClip = menuClip;
        musicSource.Play();
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

    public void CorrectSound()
    {
        sfxSource.PlayOneShot(correctSound);
    }

    public void ErrorSound()
    {
        sfxSource.PlayOneShot(errorSound);
    }
}
