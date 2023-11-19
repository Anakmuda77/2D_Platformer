using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    // Untuk movement dan Lompat
    public float movementSpeed, jumpForce;
    public bool isFacingRight, isJumping;
    Rigidbody2D rb;
    public float dashSpeed = 10f;
    private bool isDashing = false;
    public float dashDuration = 0.5f;
    private float dashTime;
    private float originalGravityScale; // Store the original gravity scale
    // Untuk Ground Checker
    public float radius;
    public Transform groundChecker;
    public LayerMask WhatIsGround;

    //Animation
    Animator anim;
    string walk_parameter = "Walk";
    string iddle_parameter = "Idle";
    string jump_parameter = "Jump";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        anim=GetComponent<Animator>();
    }
    void Start()
    {
        
    }



    void Update()
    {
        Jump();
        Movement(); // Call the Dash function in the Update method
    }

    private void FixedUpdate()
    {

    }

    void Movement()
{
    float move = Input.GetAxisRaw("Horizontal");

    // Check for left shift key to initiate dash
    if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
    {
        isDashing = true;
        dashTime = Time.time;

        // Disable gravity while dashing
        rb.gravityScale = 0f;

        // Normalize the dash direction to move in a straight line
        float dashDirection = isFacingRight ? 1f : -1f;
        Vector2 normalizedDashDirection = new Vector2(dashDirection, 0f).normalized;
        rb.velocity = normalizedDashDirection * dashSpeed;
    }

    if (isDashing)
    {
        // Check if dash time has expired
        if (Time.time >= dashTime + dashDuration)
        {
            isDashing = false;
            rb.velocity = new Vector2(0f, rb.velocity.y); // Stop dashing
            rb.gravityScale = originalGravityScale; // Restore the original gravity scale
        }
    }
    else
    {
        rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);

        if (move != 0)
        {
            anim.SetTrigger(walk_parameter);
        }
        else
        {
            anim.SetTrigger(iddle_parameter);
        }

        if (move > 0 && !isFacingRight)
        {
            transform.eulerAngles = Vector2.zero;
            isFacingRight = true;
        }
        else if (move < 0 && isFacingRight)
        {
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight = false;
        }
    }
}

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())                                    // Fungsi agar player bisa melompak dengan menekan spasi
        {
            rb.velocity = Vector2.up * jumpForce;
        }


        if(!IsGrounded() && !isJumping) {
            anim.SetTrigger(jump_parameter);
            isJumping= true;

        }

        else if(IsGrounded() && isJumping)
        {
            isJumping = false;
        }
    }

    IEnumerator DashCooldown()
    {
        // Add a cooldown to prevent continuous dashing
        yield return new WaitForSeconds(1f);
    }

    bool IsGrounded()                                                            //Fungsi mengecek apakah player ada di darat atau tidak
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, WhatIsGround);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radius);
    }
}