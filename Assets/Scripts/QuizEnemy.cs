using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnemy : MonoBehaviour {

	public GameObject quizPrefab;

	protected bool hasStarted;
	public GameObject quiz;
	protected GameObject player;
	protected GameObject mCamera;

	// Use this for initialization
	void Start () {
		hasStarted = false;
		quiz = GameObject.FindGameObjectWithTag ("Quiz"); //gets the quiz
		quiz.SetActive(false);
		player = GameObject.FindGameObjectWithTag ("Player"); //gets the player
		mCamera = GameObject.FindGameObjectWithTag("MainCamera"); 

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.gameObject == player && hasStarted == false) {
			hasStarted = true;
			player.GetComponent<Player> ().isSinglePaused = true;
			mCamera.GetComponent<CameraController> ().speedCamera = 0;
			quiz.SetActive(true);
		}
	}
}
