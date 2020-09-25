using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private SceneFlow sceneFlow;
    public bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading) {
            GetComponent<Image>().color += new Color(0, 0, 0, 1*Time.deltaTime);
            if (GetComponent<Image>().color.a >= 0.99) {
                sceneFlow.ReloadScene();
            }
        }
    }
}
