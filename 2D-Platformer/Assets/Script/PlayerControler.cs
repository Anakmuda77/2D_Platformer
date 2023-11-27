using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool canMove = true; // Flag to check if the player can move
    private int jumpsRemaining = 2;
    public float radius;
    public Transform groundChecker;
    public LayerMask WhatIsGround;

    //Animation
    Animator anim;
    string walk_parameter = "Walk";
    string iddle_parameter = "Idle";
    string jump_parameter = "Jump";

    // Knock Back Effect
    public float KBForce;       //Kekuatan knock bacnk
    public float KBCounter;
    public float KBTotalTime;


    public bool KnockFromRight;

    // Respawn
    public Vector2 respawnPoint;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        anim=GetComponent<Animator>();
    }
    void Start()
    {
        respawnPoint = transform.position;      //Posisi awal dari player

    }



    void Update()
    {
        Jump();
        Movement(); // Call the Dash function in the Update method
    }

void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (!canMove)
            return; // Skip processing input during the dash

        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

        // Check for left shift key to initiate dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash());
        }

    if (isDashing)
    {
        // Check if dash time has expired
        if (Time.time >= dashTime + dashDuration)
        {
            isDashing = false;

            // Stop dashing by removing the force
            rb.velocity = new Vector2(0f, rb.velocity.y);

            // Restore the original gravity scale
            rb.gravityScale = originalGravityScale;
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
        // Check for spacebar input to initiate jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check if the player is on the ground or has remaining jumps
            if (IsGrounded() || jumpsRemaining > 0)
            {
                // Perform the jump
                rb.velocity = Vector2.up * jumpForce;
                isJumping = true;

                // If the player is in the air, decrease the remaining jumps
                if (!IsGrounded())
                {
                    jumpsRemaining--;
                }
            }
        }

        // Check if the player is on the ground and reset the jumpsRemaining counter
        if (IsGrounded())
        {
            isJumping = false;
            jumpsRemaining = 2;
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

    IEnumerator Dash()
    {
        isDashing = true;
        dashTime = Time.time;

        // Disable further input while dashing
        canMove = false;

        // Disable gravity while dashing
        rb.gravityScale = 0f;

        // Normalize the dash direction to move in a straight line
        float dashDirection = isFacingRight ? 1f : -1f;
        Vector2 normalizedDashDirection = new Vector2(dashDirection, 0f).normalized;

        // Set the velocity to the maximum dash speed immediately
        rb.velocity = normalizedDashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        // Stop dashing
        isDashing = false;

        // Restore the original gravity scale
        rb.gravityScale = originalGravityScale;

        // Ensure the velocity is set to zero after the dash
        rb.velocity = Vector2.zero;

        // Enable input after the dash is complete
        canMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radius);
    }


    private void OnTriggerEnter2D(Collider2D collision)    //Untuk gola condition mendapatkan HollyWater
    {
        if (collision.CompareTag("Water"))
        {
            GoalMananger.singleton.CollectHollyWater(); //Menjalakan fungsi dari GoalMananger Script
            Destroy(collision.gameObject);              //Menghacurkan game object setelah disentuh
        }
        else if (collision.CompareTag("Goal"))
        {
            if (GoalMananger.singleton.canEnter)
            {
                print("YOU WIN BROH");
            }
        }
    }
}
