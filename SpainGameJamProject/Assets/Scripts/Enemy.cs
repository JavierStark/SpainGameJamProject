using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float branchDetectionDistance = 50f;
    [SerializeField] private LayerMask branchesLayer;

    [SerializeField] UserPreferences preferences;

    Animator animator;
    [SerializeField] SceneFlow sceneFlow;
    [SerializeField] AudioClip[] sounds;
    AudioSource audioSource;

    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float force;

    private bool jumping = false;
    private bool actionDone = false;
    private bool eventOn = false;
    [SerializeField] float initialAngle;


    public delegate void EnemyEvent();
    public event EnemyEvent enemyDeadEvent;


    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = preferences.fXVolume;
        
        StartCoroutine(BehaviourLoop());
    }

    private void Update() {
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(target);
    }


    private IEnumerator BehaviourLoop() {
        yield return Shoot();
        while (true){            
            if (actionDone) {
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
                actionDone = false;
                yield return Shoot();           
            }
            
        }
    }
    ////private List<Branch> DetectNearBranches() {
    //    List<Branch> nearBranches = new List<Branch>();

    //    foreach(Collider col in Physics.OverlapSphere(this.transform.position, branchDetectionDistance, branchesLayer)){
    //        if (col.gameObject.GetComponent<Branch>() && col.gameObject.transform.position != this.transform.position) {

    //            Ray ray = new Ray(transform.position, col.gameObject.transform.position - transform.position);
    //            RaycastHit[] hits = Physics.RaycastAll(ray, branchDetectionDistance);


    //            foreach(RaycastHit hit in hits) {
    //                if(hit.transform.gameObject.CompareTag("Tree")) {
    //                    Debug.Log(hit.transform.gameObject.tag);
    //                    return null;
    //                }
    //            }

    //            nearBranches.Add(col.gameObject.GetComponent<Branch>());

    //        }
    //    }

    //    return nearBranches;
    //}
    
    private IEnumerator Shoot() {

        Ray ray = new Ray(transform.position, (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position));        
        Debug.DrawRay(ray.origin, ray.direction);
        RaycastHit[] hits = Physics.RaycastAll(ray,100);

        bool posissibleShoot = true;
        
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Tree")) {
                Debug.Log("Dont shoot");
                posissibleShoot = false;                
            }
        }

        if (posissibleShoot) {
            animator.SetTrigger("Throw");
        }

        yield return new WaitUntil(() => actionDone == true);
    }

    public void ShootEvent() {
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.Play();
        var currentProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
        Destroy(currentProjectile, 4f);

        Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position);
        currentProjectile.GetComponent<Rigidbody>().AddForce(direction*force, ForceMode.Impulse);
        actionDone = true;
    }

    //private IEnumerator Jump(List<Branch> branches) {
    //    if (branches != null) {
    //        Debug.Log("Jump");
    //        yield return JumpToBranch(branches[Random.Range(0, branches.Count)].center);
    //    }
    //    actionDone = true;
    //}

    //private IEnumerator JumpToBranch(Transform branch) {
    //    var rigid = GetComponent<Rigidbody>();

    //    Vector3 p = new Vector3(branch.position.x , (branch.position.y + branch.localScale.y) , branch.position.z);


    //    float gravity = Physics.gravity.magnitude;
    //    // Selected angle in radians
    //    float angle = initialAngle * Mathf.Deg2Rad;

    //    // Positions of this object and the target on the same plane
    //    Vector3 planarTarget = new Vector3(p.x, 0, p.z);
    //    Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

    //    // Planar distance between objects
    //    float distance = Vector3.Distance(planarTarget, planarPostion);
    //    // Distance along the y axis between objects
    //    float yOffset = transform.position.y - p.y;

    //    float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

    //    Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

    //    // Rotate our velocity to match the direction between the two objects
    //    float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
    //    Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

    //    animator.Play("Jump");

    //    yield return new WaitUntil(() => eventOn);
    //    // Fire!
    //    if (!float.IsNaN(finalVelocity.x) || !float.IsNaN(finalVelocity.y) || !float.IsNaN(finalVelocity.z)) {
    //        rigid.velocity = finalVelocity;
    //        jumping = true;
    //    }
    //    eventOn = false;
    //    yield return new WaitWhile(() => jumping == true);
    //}

    //public void JumpEvent() {
    //    eventOn = true;
    //}

    private void OnTriggerEnter(Collider collider) {
        if (collider.transform.CompareTag("DeadTrigger")) {
            enemyDeadEvent?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, branchDetectionDistance);
    }
}
