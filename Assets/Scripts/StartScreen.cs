using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("start");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touches.Length>0) {
			Debug.Log ("You tapped");
		}
	}
}
