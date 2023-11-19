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

    //Abilities
    private float _dashingVelocity = 14f;
    private float _dashingTime = 0.5f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;
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
        var dashInput = Input.GetKeyDown(KeyCode.LeftShift);

        if (dashInput && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }

        if (_isDashing)
        {
            rb.velocity = _dashingDir.normalized * _dashingVelocity;
            return;
        }

        if (IsGrounded())
        {
            _canDash = true;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _isDashing = false;
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
