using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Effect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float scale = 0.95f;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }

	public void OnPointerDown(PointerEventData evenData)
	{
        if(button.interactable)
        {
        transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    public void OnPointerUp(PointerEventData evenData)
	{
        if(button.interactable)
        {
        transform.localScale = new Vector3(1, 1, 1);
        }
	}
}