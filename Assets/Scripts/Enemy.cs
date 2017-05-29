using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enemy : MonoBehaviour {

	public bool isWalkingRight;
	protected Rigidbody2D rb;

	public float moveSpeed = 2;
	protected float waitForDeathPopupSeconds = 0.5f;
	protected int collisionCount = 0;
	protected int noEnemies; //used to disable enemies being harmful

	public bool isGrounded;


    protected PopupController popupScript;

	// Use this for initialization
	void Start () {
		noEnemies = PlayerPrefs.GetInt ("noEnemies", 0);
		isWalkingRight = true;
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
		if (coll.gameObject.name == "Player" && noEnemies == 0) { 
			//wait a second
			StartCoroutine(ShowDeathScreen());
		}
	}

	public void Reverse()
	{
		isWalkingRight = !isWalkingRight;
		transform.Rotate(new Vector3(0,180,0)); //flip the enemy
	}

	IEnumerator ShowDeathScreen() //using coroutine to wait a while before showing the death screen
	{
		yield return new WaitForSeconds(waitForDeathPopupSeconds);
		popupScript.GameOverPopUpDeath ();
	}
}
