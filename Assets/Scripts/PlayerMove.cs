using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    [Header("Jumping Mech")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool isReadyToJump = true;
    [Header("Ground Check")]
    public float groundDrag;
    public Transform groundCheck; // player legs
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded;

    Vector3 moveDirection;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // cannot enable this in inspector, doing it in the code
        rb.freezeRotation = true;
    }
    void Update()
    {
        // basic movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // jump
        if(Input.GetKey(KeyCode.Space) && isReadyToJump && isGrounded)
        {
            isReadyToJump = false;
            JumpPlayer();
            Invoke("JumpReset", jumpCooldown);
        }
        // speed regulator
        SpeedControl();
        // ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 15f, ForceMode.Force);
        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 15f * airMultiplier, ForceMode.Force);

        }
    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void JumpPlayer()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void JumpReset()
    {
        isReadyToJump = true;
    }
    public void TempDisableVelocity(float duration)
    {
        rb.isKinematic = true;
        Invoke("EnableVelocity", duration);
    }
    void EnableVelocity()
    {
        rb.isKinematic = false;
    }
}
