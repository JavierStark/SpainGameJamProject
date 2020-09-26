using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Values : MonoBehaviour
{
    public UserPreferences preferences;

    public Slider music, fX;

    void Start()
    {
        music.value = preferences.musicVolume;
        fX.value = preferences.fXVolume;
    }

    void Update()
    {
        preferences.musicVolume = music.value;
        preferences.fXVolume = fX.value;
    }
}
