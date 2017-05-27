using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour {

	void Start () {
		if (PlayerPrefs.GetString("firstTimeCanvas", "notOpened") == "notOpened" ) {
			Handheld.PlayFullScreenMovie ("intro.mp4", Color.black, FullScreenMovieControlMode.Full); 
		}
	}
}
