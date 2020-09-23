using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnimEvents : MonoBehaviour
{
    public void FruitDrop() {
        GetComponentInParent<ProjectileLauncher>().FruitDrop();
    }

    public void ReadyToShoot() {
        GetComponentInParent<ProjectileLauncher>().ReadyToShoot();
    }

    public void GetFruitToEat() {
        GetComponentInParent<ProjectileLauncher>().GetFruitToEat();
    }

    public void StaminaUp() {
        FindObjectOfType<Stamina>().GetStamina(GetComponentInParent<ProjectileLauncher>().projectilePrefab.GetComponent<Projectile>().staminaToRegenerate);
    }
}
