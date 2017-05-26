using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Handheld.PlayFullScreenMovie ("test.mp4", Color.black, FullScreenMovieControlMode.Full); //FullScreenMovieControlMode.CancelOnInput);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
