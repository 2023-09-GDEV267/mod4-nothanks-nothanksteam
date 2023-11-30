using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Diagnostics.Tracing;

public class MusicManager : MonoBehaviour
{
    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip scoringClip;
    public float fadeTime;

    private AudioSource source;
    private AudioClip currentClip;
    private bool fadeIn;
    private bool fadeOut;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
        source.clip = menuClip;
        fadeIn = true;
        fadeOut = false;
        currentClip = menuClip;
        source.Play();
    }

    void Update()
    {
        if (fadeIn && source.volume < 1)
        {
            source.volume += Time.deltaTime / fadeTime;
            if (source.volume >= 1)
            {
                source.volume = 1;
                fadeIn = false;
            }
        }

        if (fadeOut && source.volume > 0)
        {
            source.volume -= Time.deltaTime / fadeTime;
            if (source.volume <= 0)
            {
                source.volume = 0;
                source.Stop();
                source.clip = currentClip;
                source.Play();
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
}
