using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip menuClip;
    public AudioClip gameClip;
    public AudioClip scoringClip;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
        MenuMusic();
    }

    public void MenuMusic()
    {
        FadeOut(2);
        source.Stop();
        source.clip = menuClip;
        source.Play();
        FadeIn(2);
    }

    public void GameMusic()
    {
        FadeOut(2);
        source.Stop();
        source.clip = gameClip;
        source.Play();
        FadeIn(2);
    }

    public void ScoringMusic()
    {
        FadeOut(2);
        source.Stop();
        source.clip = scoringClip;
        source.Play();
        FadeIn(2);
    }

    private void FadeOut(float fadeTime)
    {
        float startVol = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVol * Time.deltaTime / fadeTime;
        }
    }

    private void FadeIn(float fadeTime)
    {
        float startVol = source.volume + 0.1f;

        while (source.volume < 0)
        {
            source.volume += startVol * Time.deltaTime / fadeTime;
        }
    }
}
