using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Slider shootForceSlider;

    private float force;


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            force += 0.3f * Time.deltaTime;
            force = Mathf.Clamp(force, 0, 1);
            shootForceSlider.value = force;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            var projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity, parent);

            projectile.transform.forward = Camera.main.transform.forward;
                        
            projectile.GetComponent<Projectile>().AddForce(force);

            force = 0;
            shootForceSlider.value = force;
        }
    }
}
