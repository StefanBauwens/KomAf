﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    protected float moveSpeed = 5;
    protected float jumpHeight = 5;
    protected short totalACount;
    protected short totalPageCount;
    protected bool isPaused;

    protected Rigidbody2D rb;
    protected Animator anim;

    public bool isGrounded;
    public bool doubleJumped;
    public bool isAgainstWall;

    public Transform groundCheck;
    public float groundCheckRadius; 
    public LayerMask groundSprite;

    public Popup popUp;



    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundSprite); // detect if player collides with ground
        anim.SetBool("isGrounded", isGrounded);

        CheckJump();

        if (isGrounded)
        {
            doubleJumped = false;
        }

        if (isAgainstWall && !isGrounded)
        {
            doubleJumped = false;
            rb.velocity = Vector2.up * 2f;    
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !doubleJumped)
        {
            Jump();
            doubleJumped = true;
        }
    }

    /* don't know how it works
    void OnBecameInvisible()
    {
        popUp.GameOverPopUp();
        Debug.Log("invisible");
    }
    */
}
