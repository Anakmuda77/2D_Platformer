using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    // Untuk movement dan Lompat
    public float movementSpeed, jumpForce;
    public bool isFacingRight, isJumping;
    Rigidbody2D rb;

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
        anim=GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    void Movement()
    {   
        float move = Input.GetAxisRaw("Horizontal");                            //Fungsi untuk menggerakkan player berdasarkan input horizontal
        rb.velocity = new Vector2(move*movementSpeed, rb.velocity.y);

        if (move != 0)                                                          //Menggerakkan animasi iddle ke walk atau sebaliknya
        {
            anim.SetTrigger(walk_parameter);
        }
        else
        {
            anim.SetTrigger(iddle_parameter);
        }


        if(move>0 && !isFacingRight)                                            //Untuk membuat player bisa berbalik kiri dan kanan
        {
            transform.eulerAngles = Vector2.zero;
            isFacingRight= true;
        } 
        else if(move<0 && isFacingRight)
        {
            transform.eulerAngles = Vector2.up * 180;
            isFacingRight= false; 
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



    bool IsGrounded()                                                            //Fungsi mengecek apakah player ada di darat atau tidak
    {
        return Physics2D.OverlapCircle(groundChecker.position, radius, WhatIsGround);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundChecker.position, radius);
    }
}
