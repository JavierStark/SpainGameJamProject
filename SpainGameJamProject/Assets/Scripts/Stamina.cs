using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{

    private Slider staminaBar;

    [SerializeField] private bool regenerating = false;
    [SerializeField] private float regenerationValue;
    [SerializeField] private float staminaLostPerSecond;

    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value -= staminaLostPerSecond*Time.deltaTime;
        if (staminaBar.value == 0) {
            GetComponent<Animator>().Play("StaminaEmpty");
        }
        else {
            GetComponent<Animator>().Play("Default");
        }
    }

    
    public void SpendStamina(float spentStamina) {
        staminaBar.value -= spentStamina;
    }

    public void GetStamina(float getStamina) {
        staminaBar.value += getStamina;
    }
    
    public float StaminaValue() {
        return staminaBar.value;
    }

}
