using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The code in this script, as well as the game object(s) that work alongside
//this script, are simple placeholders for what's to come, later in development.
//Once the inclusion is polished, this can be reworked, accordingly.

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip correct, wrong;

    public void CorrectButton()
    {
        src.clip = correct;
        src.Play();
    }

    public void WrongButton()
    {
        src.clip = wrong;
        src.Play();
    }
}