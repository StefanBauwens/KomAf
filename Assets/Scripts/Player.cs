using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    protected float moveSpeed = 5;
    protected float jumpHeight = 5;
    protected short totalACount;
    protected short totalPageCount;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer spriteR;

    public bool isGrounded;
    public bool doubleJumped;
    public bool isAgainstObject;
    public bool isPaused;
    public bool inReverseDirection = false;

    public Transform groundCheck;
    public float groundCheckRadius; 
    public LayerMask groundSprite;

    public PopupManager popUpM;



    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded && !isPaused) //(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGrounded && !doubleJumped && !isPaused)
        {
            Jump();
            doubleJumped = true;
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
        popUpM.GameOverPopUp();
    }
    
}
