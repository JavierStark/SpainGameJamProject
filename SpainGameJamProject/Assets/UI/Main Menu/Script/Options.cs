using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [HideInInspector]
    public bool optionsActiv = false;

    public GameObject optionsUI;


    void Update()
    {
        if (optionsActiv)
        {
            optionsUI.SetActive(true);
        }
        else
        {
            optionsUI.SetActive(false);
        }
    }

    public void Action()
    {
        optionsActiv = !optionsActiv;
    }
}
