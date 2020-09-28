using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float force = 50;
    [SerializeField] public float staminaToRegenerate;
    private AudioSource audioSource;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] UserPreferences preferences;


    private bool recharging = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        if (transform.parent?.name == "Projectiles") {

            audioSource.volume = preferences.fXVolume;
        }

    }

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
        if (other.CompareTag("FruitCollector")) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-collision.GetContact(0).normal*1000, collision.GetContact(0).point);
        }
        else if (transform.parent?.name == "Projectiles") {
            if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Brach")) {
                audioSource.clip = clip2;
                audioSource.Play();
            }
            else if (collision.gameObject.CompareTag("Enemy")) {
                audioSource.clip = clip1;
                audioSource.Play();
                collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-collision.GetContact(0).normal*1000, collision.GetContact(0).point);

            }
        }
    }
}
