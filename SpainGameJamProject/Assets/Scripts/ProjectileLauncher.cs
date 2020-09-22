using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] private Transform parent;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            var projectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity, parent);

            projectile.transform.forward = Camera.main.transform.forward;
        }
    }
}
