using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UserPreferences : ScriptableObject
{
    public bool mute;
    public float musicVolume, fXVolume;
    public int difficulty;
}
