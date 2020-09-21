using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float jumpForce = 3f;
    private float jumpMultiplier = 0;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask propMask;
    private Rigidbody playerRigidbody;

    private float horizontalInput;
    private float verticalInput;
    


    private Vector3 velocity;
    private bool isGrounded;
    private float groundDistance = 0.35f;
    private bool isAbleToJump;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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
            jumpMultiplier += Time.deltaTime*2;
        }

        if (Input.GetAxis("Jump") == 0) {
            Jump();
            jumpMultiplier = 0;
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

        if(isGrounded)
        playerRigidbody.AddForce(Vector3.up*jumpForce*jumpMultiplier);
    }
}
