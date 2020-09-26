using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool pause;
    public KeyCode key = KeyCode.Escape;

    public GameObject pauseMenu;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Pausar();
        }

        if (pause)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    public void Pausar()
    {
        pause = !pause;
    }
}
