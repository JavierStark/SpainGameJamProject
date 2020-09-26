using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    List<Branch> branches;

    [SerializeField] GameObject monkey;

    // Start is called before the first frame update
    void Start()
    {
        branches = FindObjectsOfType<Branch>().ToList<Branch>();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() {
        while (true) {
            yield return new WaitForSeconds(2f);            
            List<Branch> emptyBranches = new List<Branch>();
            foreach(Branch b in branches) {
                if(b.enemyOn == null) {
                    emptyBranches.Add(b);
                }
            }
            if (emptyBranches != null) {
                Branch branch = emptyBranches[Random.Range(0, emptyBranches.Count)];
                Vector3 position = new Vector3(branch.center.position.x, branch.center.position.y /*+ monkey.transform.localScale.y+*/, branch.center.position.z);
                var m = Instantiate(monkey, position, Quaternion.identity);
                branch.enemyOn = m;
            }
        }
    }
}
