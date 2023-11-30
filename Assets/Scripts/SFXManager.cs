using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    [SerializeField] Slider SoundEffectsVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SoundEffectsVolume"))
        {
            PlayerPrefs.SetFloat("SoundEffectsVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeSFXVolume()
    {
        AudioListener.volume = SoundEffectsVolumeSlider.value;
        Save();
    }

    private void Load()
    {
        SoundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", SoundEffectsVolumeSlider.value);
    }
}