using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrontCheck : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Obstacle") {
			this.transform.parent.gameObject.GetComponent<Enemy>().Reverse ();
		}
	}
}
