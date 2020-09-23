using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float force = 50;
    [SerializeField] public float staminaToRegenerate;

    private bool recharging = false;

    private void Update() {
        transform.Rotate(new Vector3(0, 100, 0)*Time.deltaTime);
        if (recharging) {
            transform.position = transform.parent.position;
        }
    }

    public void AddForceToProjectile(float multiplier) {
        GetComponent<Rigidbody>().AddForce(transform.forward * force * multiplier, ForceMode.Impulse);
    }

    public void Recharging() {
        recharging = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FruitCollector") {
            Debug.Log("Trigger");
            Destroy(gameObject);
        }
    }
}
