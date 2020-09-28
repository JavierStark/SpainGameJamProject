using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetInt("Sensitivity");
        if(slider.value == 0) {
            slider.value = 100f;
            ChangeSensitivity();
        }
    }

    
    public void ChangeSensitivity() {
        PlayerPrefs.SetInt("Sensitivity", (int)slider.value);
    }


}
