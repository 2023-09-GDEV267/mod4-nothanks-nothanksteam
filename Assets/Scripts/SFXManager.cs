using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip cardFlip;
    public AudioClip tokenClink;
    public float randomPitchMin;
    public float randomPitchMax;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void FlipCardSound()
    {
        source.pitch = Random.Range(randomPitchMin, randomPitchMax);
        source.PlayOneShot(cardFlip);
        source.pitch = 1f;
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
        source.pitch = Random.Range(randomPitchMin, randomPitchMax);
        source.PlayOneShot(tokenClink);
        source.pitch = 1f;
    }

    public void CollectTokensSound(int numOfTokens)
    {
        for (float i = 0; i < (numOfTokens * 0.05); i += 0.05f)
        {
            Invoke("PlaceToken", i);
        }
    }
}
