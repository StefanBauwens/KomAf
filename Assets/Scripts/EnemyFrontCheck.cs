using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrontCheck : MonoBehaviour {
	protected int timer = 0;

	void Update() {
		if (timer>0) { //this prevents the enemy from getting hooked
			timer--;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Obstacle" && timer == 0) {
			timer = 4;
			this.transform.parent.gameObject.GetComponent<Enemy>().Reverse ();
		}
	}
}
