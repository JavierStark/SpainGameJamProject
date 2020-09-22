using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float force = 50;

    

    public void AddForce(float multiplier) {
        GetComponent<Rigidbody>().AddForce(transform.forward * force * multiplier, ForceMode.Impulse);
    }
}
