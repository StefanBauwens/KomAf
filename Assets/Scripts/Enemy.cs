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
    public GameObject warningSign;
    //public GameObject warningSignBlue;
    protected GameObject followSign;


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
        if(followSign != null)
        {
            followSign.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.7f, -2.5f);
        }  

    }

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player" && noEnemies == 0) {
            //wait a second
            if (followSign == null)
            {
                followSign = Instantiate(warningSign, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.7f, -2.5f), Quaternion.identity);
            }

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
        Destroy(followSign);
	}
}
