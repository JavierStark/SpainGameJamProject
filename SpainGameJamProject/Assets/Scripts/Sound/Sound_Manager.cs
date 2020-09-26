using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound_Manager : MonoBehaviour
{
    private AudioSource audio;
    public bool music;
    public UserPreferences preferences;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (music)
        {
            audio.volume = preferences.musicVolume;
        }
        else
        {
            audio.volume = preferences.fXVolume;
        }

        if (preferences.mute)
        {
            audio.mute = true;
        }
        else
        {
            audio.mute = false;
        }
    }
}
