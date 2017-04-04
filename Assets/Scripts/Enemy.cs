using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	//protected Animator anim;
	public bool isWalkingRight; //change back to protected after debugging!
	protected Rigidbody2D rb;

	public float moveSpeed = 2;
	protected int collisionCount = 0;

	public bool isGrounded;

    protected PopupController popupScript;

	// Use this for initialization
	void Start () {
		isWalkingRight = true;
		//anim = gameObject.GetComponent<Animator> ();
		rb 	 = gameObject.GetComponent<Rigidbody2D> ();
        popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
		isGrounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2(moveSpeed*(isWalkingRight?1:-1), rb.velocity.y);
		if (!isGrounded) {
			isGrounded = true;
			isWalkingRight = !isWalkingRight;
			transform.Rotate(new Vector3(0,180,0)); //flip the enemy
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player") {
            popupScript.GameOverPopUp();
		} else {
			Reverse ();
		}
	}

	void Reverse()
	{
		isWalkingRight = !isWalkingRight;
		transform.Rotate(new Vector3(0,180,0)); //flip the enemy
	}
}
