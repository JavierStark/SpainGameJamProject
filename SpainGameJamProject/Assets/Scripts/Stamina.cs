using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{

    private Slider staminaBar;

    [SerializeField] private bool regenerating = false;
    [SerializeField] private float regenerationValue;

    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(regenerating) {
            staminaBar.value += regenerationValue * Time.deltaTime;
        }        
    }

    
    public void SpendStamina(float spentStamina) {
        staminaBar.value -= spentStamina;
    }
    
}
