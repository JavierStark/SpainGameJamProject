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
}
