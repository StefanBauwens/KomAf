﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer spriteR;

    public float jumpHeight = 5;
    public float moveSpeed = 5;

    public bool isGrounded;
    public bool doubleJumped;
    public bool isAgainstObject;
    public bool isPaused;
    public bool inReverseDirection;
    public bool jumpHigher;

    public Transform groundCheck;
    public float groundCheckRadius; 
    public LayerMask groundSprite;

    public PopupController popupC;



    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
	}

    void FixedUpdate()
    {
        ChangeDirection();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundSprite); // detect if player collides with ground
        anim.SetBool("isGrounded", isGrounded);

        CheckIsGrounded();
        CheckJump();
        CheckAgainstObject();
    }

    void Jump()
    {
        if (inReverseDirection)
        {
            rb.velocity = new Vector2(-rb.velocity.x, jumpHeight);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }  
    }


    void CheckIsGrounded()
    {
        if (isGrounded)
        {
            doubleJumped = false;
        }
    }

    void CheckJump()
    {
		if (!isPaused) {
			if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && isGrounded) //(Input.GetKeyDown(KeyCode.Space) && isGrounded)
			{
				Jump();
			}
			if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && !isGrounded && !doubleJumped)
			{
				Jump();
				doubleJumped = true;
			}
		}
    }

    void CheckAgainstObject()
    {
        if (isAgainstObject && !isGrounded)
        {
            doubleJumped = false;
            rb.velocity = Vector2.up * 2f;
        }
    }

    void ChangeDirection()
    {
        if (inReverseDirection)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1,1,1); // flip player in reverse direction
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = Vector3.one;
        }
    }

    void OnBecameInvisible()
    {
        popupC.GameOverPopUp();
    }
    
}
