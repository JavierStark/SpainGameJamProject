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
    }

    public void Pausar()
    {
        pause = !pause;
        pauseMenu.SetActive(pause);

        if (pause) {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else { 
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
