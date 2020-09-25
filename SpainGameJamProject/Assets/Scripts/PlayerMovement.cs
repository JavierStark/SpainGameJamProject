using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //References
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask propMask;
    [SerializeField] private Slider jumpSlider;
    [SerializeField] private Stamina stamina;
    [SerializeField] private Fade fade;
    private Rigidbody playerRigidbody;

    //Axis
    private float horizontalInput;
    private float verticalInput;
    
    //Jump
    [SerializeField] private float jumpForce = 200f;
    private float jumpMultiplier = 1;

    //Movement
    private Vector3 velocity;
    [SerializeField] private float playerSpeed = 10f;

    //Grounded
    private bool isGrounded;
    private float groundDistance = 0.35f;

    [SerializeField] private float climbForce;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        jumpSlider.gameObject.SetActive(false);
    }

    private void CheckGround()
    {
        if(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, propMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        ReadInputs();
        CheckGround();
        Movement();
    }

    private void ReadInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if (Input.GetAxis("Jump") > 0.1)
        {
            jumpSlider.gameObject.SetActive(true);

            jumpMultiplier += 1f * Time.deltaTime*2;
            jumpMultiplier = Mathf.Clamp(jumpMultiplier, 1, 10);
            jumpSlider.value = jumpMultiplier;
        }

        if (Input.GetButtonUp("Jump")) {
            if (isGrounded) {
                Jump();
                jumpSlider.gameObject.SetActive(false);
            }
            jumpMultiplier = 1;
            jumpSlider.value = jumpMultiplier;
        }
    }

    private void Movement()
    {
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rigthMovement = transform.right * horizontalInput;

        if (horizontalInput != 0 || verticalInput != 0) {
            Vector3 movementDirection = Vector3.ClampMagnitude(forwardMovement + rigthMovement, 1.0f);
            playerRigidbody.MovePosition(transform.position + movementDirection * playerSpeed * Time.deltaTime);
        }
        else {
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
        }
    }

    private void Jump() {
        playerRigidbody.AddForce(Vector3.up*jumpForce*jumpMultiplier);
        stamina.SpendStamina(20);        
    }



    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("ClimbCollider")) {
            stamina.SpendStamina(30);

            playerRigidbody.AddForce(Vector3.up * climbForce);

            other.gameObject.SetActive(false);

            StartCoroutine(ActivateClimbColliderAfterDelay(other.gameObject));
        }
        else if (other.gameObject.CompareTag("DeadTrigger")) {
            Debug.Log("fade");
            fade.fading = true;
        }
    }

    private IEnumerator ActivateClimbColliderAfterDelay(GameObject climbCollider) {
        yield return new WaitForSeconds(1f);
        climbCollider.SetActive(true);
    }
}
