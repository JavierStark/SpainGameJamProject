using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float force = 50;

    private void Update() {
        transform.Rotate(new Vector3(0, 100, 0)*Time.deltaTime);
    }

    public void AddForceToProjectile(float multiplier) {
        GetComponent<Rigidbody>().AddForce(transform.forward * force * multiplier, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        Destroy(gameObject);
    }
}
