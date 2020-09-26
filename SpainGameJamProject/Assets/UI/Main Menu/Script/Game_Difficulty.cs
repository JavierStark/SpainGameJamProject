using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Difficulty : MonoBehaviour
{
    [HideInInspector]
    public int difficulty = 1;
    public Text difficultyText;
    public Button next, back;
    public UserPreferences preferences;

    void Update()
    {
        if (preferences.difficulty == 0)
        {
            difficultyText.text = "Novato".ToString();
            back.interactable = false;
            next.interactable = true;
        }
        else if (preferences.difficulty == 1)
        {
            difficultyText.text = "Normal".ToString();
            back.interactable = true;
            next.interactable = true;
        }
        else
        {
            difficultyText.text = "Extremo".ToString();
            next.interactable = false;
            back.interactable = true;
        }
    }

    public void Next()
    {
        preferences.difficulty += 1;
    }
    public void Back()
    {
        preferences.difficulty -= 1;
    }
}
