using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField] public Transform center;
    public Enemy enemyOn;

    void EnemyDead() { 
        enemyOn = null;
    }

    public void SetMonkey(Enemy enemy) {
        enemyOn = enemy;
        enemyOn.enemyDeadEvent +=  EnemyDead;
    }
}
