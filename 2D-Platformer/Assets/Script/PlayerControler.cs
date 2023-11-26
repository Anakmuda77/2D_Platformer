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

    // Untuk Ground Checker
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
        anim=GetComponent<Animator>();
    }
    void Start()
    {
        respawnPoint = transform.position;      //Posisi awal dari player

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
        float move = Input.GetAxisRaw("Horizontal");     //Fungsi untuk menggerakkan player berdasarkan input horizontal

        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(move * movementSpeed, rb.velocity.y);

        }
        else
        {
            if(KnockFromRight == true)                          //Memberikan knockback bila dikanan
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);    // Mmeberikan knockback bila di kiri
            }

            KBCounter -= Time.deltaTime;
        }

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

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
             }

            }

           
        }
         
         
     }
    


