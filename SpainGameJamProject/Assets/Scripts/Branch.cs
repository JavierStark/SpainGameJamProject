using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField] public Transform center;
    public GameObject enemyOn;

    private void OnCollisionExit(Collision collision) {
        enemyOn = null;
    }
}
