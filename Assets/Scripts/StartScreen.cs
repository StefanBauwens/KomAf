using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {
	public string LevelToLoad;
	
	// Update is called once per frame
	void Update () {
		if (Input.touches.Length>0) {
			SceneManager.LoadScene (LevelToLoad);
		}
	}
}
