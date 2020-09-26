using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public UserPreferences preferences;


    public Button audio, mute;

    void Start()
    {
        
    }

    void Update()
    {
        if (preferences.mute)
        {
            audio.interactable = true;
            mute.interactable = false;
        }
        else
        {
            audio.interactable = false;
            mute.interactable = true;
        }
    }

    public void NoMute()
    {
        preferences.mute = false;

        audio.interactable = false;
        mute.interactable = true;
    }
    public void Mute()
    {
        preferences.mute = true;

        audio.interactable = true;
        mute.interactable = false;
    }
}
