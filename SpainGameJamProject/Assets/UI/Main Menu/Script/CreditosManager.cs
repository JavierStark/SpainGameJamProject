using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditosManager : MonoBehaviour
{
    [TextArea(1, 20)]
    public string[] credits;

    public int creditActual;
    public int creditMax;
    private Text creditsText;

    private void Start()
    {
        creditsText = GetComponent<Text>();
    }

    void Update()
    {
        creditsText.text = credits[creditActual].ToString();
    }
    public void Next(string Scena)
    {
        creditActual += 1;
        if (creditActual == creditMax)
        {
            SceneManager.LoadScene(Scena);
        }
    }
}
