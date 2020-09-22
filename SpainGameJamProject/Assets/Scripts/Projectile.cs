using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float force = 20;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force , ForceMode.Impulse);
    }
}
