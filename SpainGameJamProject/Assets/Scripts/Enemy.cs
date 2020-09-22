using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float branchDetectionDistance = 50f;
    [SerializeField] private LayerMask branchesLayer;

    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float force;


    void Start()
    {
        StartCoroutine(BehaviourLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Branch> DetectNearBranches() {
        List<Branch> nearBranches = new List<Branch>();

        foreach(Collider col in Physics.OverlapSphere(this.transform.position, branchDetectionDistance, branchesLayer)){
            if (col.gameObject.GetComponent<Branch>() && col.gameObject.transform.position != this.transform.position) {
                nearBranches.Add(col.gameObject.GetComponent<Branch>());
            }
        }

        return nearBranches;
    }

    private IEnumerator BehaviourLoop() {
        while(true){
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            if(Random.Range(0,2) == 0) {
                Jump(DetectNearBranches());
            }
            else {
                Shoot();
            }
        }
    }
    
    private void Shoot() {
        var currentProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);

        Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

        currentProjectile.GetComponent<Rigidbody>().AddForce(direction*force, ForceMode.Impulse);
    }

    private void Jump(List<Branch> branches) {
        JumpToBranch(branches[Random.Range(0, branches.Count)]);
    }

    private void JumpToBranch(Branch branch) {
        transform.position = new Vector3(branch.transform.position.x, branch.transform.position.y + branch.transform.localScale.y, branch.transform.position.z);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, branchDetectionDistance);
    }
}
