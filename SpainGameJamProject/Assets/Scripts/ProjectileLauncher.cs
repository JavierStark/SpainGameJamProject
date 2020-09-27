using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab = null;

    [SerializeField] private UserPreferences preferences;
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private Transform parent;
    [SerializeField] private Slider shootForceSlider;
    [SerializeField] private Transform fruitDropper;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private float force;

    bool readyToShoot = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = preferences.fXVolume;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && readyToShoot) {
            force += 0.3f*Time.deltaTime;
            force = Mathf.Clamp(force, 0, 1);
            shootForceSlider.value = force;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && readyToShoot) {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !readyToShoot) {
            Recharge();
        }

        if (Input.GetKeyDown(KeyCode.Q) && readyToShoot) {
            Eat();
        }
    }

    private void Shoot() {

        audioSource.clip = clips[0];
        audioSource.Play();

        var projectile = Instantiate(projectilePrefab, transform.GetChild(1).position, Quaternion.identity, parent);
        projectile.transform.forward = Camera.main.transform.forward;

        projectile.GetComponent<Projectile>().AddForceToProjectile(force);

        force = 0;
        shootForceSlider.value = force;

        readyToShoot = false;
    }

    private void Recharge() {

        audioSource.clip = clips[1];
        audioSource.Play(31000);

        projectilePrefab = projectiles[Random.Range(0, projectiles.Count)];

        GetComponentInChildren<Animator>().Play("Reload");        
    }

    private void Eat() {
        readyToShoot = false;
        GetComponentInChildren<Animator>().Play("Eat");
    }

    public void GetFruitToEat() {
        var projectile = Instantiate(projectilePrefab, fruitDropper.position, Quaternion.Euler(new Vector3(90, 0, 0)), fruitDropper);
        projectile.transform.localScale = new Vector3(70, 70, 70);
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Collider>().enabled = false;
        projectile.GetComponent<Projectile>().Recharging();
        Destroy(projectile, 0.3f);
    }

    public void FruitDrop() {                
        var projectile = Instantiate(projectilePrefab, fruitDropper.position, Quaternion.Euler(new Vector3(90, 0, 0)), fruitDropper);
        projectile.transform.localScale = new Vector3(70, 70, 70);
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Projectile>().Recharging();
    }

    public void ReadyToShoot() {
        readyToShoot = true;
    }
}
