using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileLauncher : MonoBehaviour
{
    private GameObject projectilePrefab = null;
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private Transform parent;
    [SerializeField] private Slider shootForceSlider;
    [SerializeField] private Transform fruitDropper;

    private float force;

    bool readyToShoot = false;


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && readyToShoot) {
            force += 0.3f * Time.deltaTime;
            force = Mathf.Clamp(force, 0, 1);
            shootForceSlider.value = force;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && readyToShoot) {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !readyToShoot) {
            Recharge();
        }
    }

    private void Shoot() {
        var projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity, parent);

        projectile.transform.forward = Camera.main.transform.forward;

        projectile.GetComponent<Projectile>().AddForceToProjectile(force);

        force = 0;
        shootForceSlider.value = force;

        readyToShoot = false;
    }

    private void Recharge() {       

        projectilePrefab = projectiles[Random.Range(0, projectiles.Count)];

        GetComponentInChildren<Animator>().Play("Reload");        
    }

    public void FruitDrop() {
        GameObject prefab = projectilePrefab;
        prefab.transform.localScale = new Vector3(100,100,100);
        var projectile = Instantiate(projectilePrefab, fruitDropper.position, Quaternion.Euler(new Vector3(90, 0, 0)), transform);
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Rigidbody>().AddForce((transform.GetChild(0).gameObject.transform.position - projectile.transform.position)*100);
    }

    public void ReadyToShoot() {
        readyToShoot = true;
    }
}
