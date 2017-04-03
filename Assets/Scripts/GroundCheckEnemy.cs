using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckEnemy : MonoBehaviour {

	protected GameObject parent;
	protected bool isGrounded;
	protected int count;

	void Start () {
		parent = this.transform.parent.gameObject;
		isGrounded = true;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGrounded) {
			count++;
		}
		//I use a counter because else every time I exit a tile and go on a tile with the same name it still thinks for a second that i need to reverse
		if (count>1) { 
			parent.GetComponent<Enemy> ().isGrounded = isGrounded;
			isGrounded = true;
			count = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.name == "ground8x8")
		{
			isGrounded = false;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.name == "ground8x8")
		{
			isGrounded = true;
			count = 0;
		}
	}
}
