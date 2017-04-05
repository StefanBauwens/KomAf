using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrontCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "ground8x8") {
			this.transform.parent.gameObject.GetComponent<Enemy>().Reverse ();
		}
	}
}
