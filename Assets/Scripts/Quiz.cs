﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Quiz : MonoBehaviour {

	public Text popupText;
	public string[] sentences; 
	protected string[] newSentences;
	public float typeSpeed; // I suggest using something like 0.07

	public string easyQuestion;
	public string hardQuestion;

	public string[] answersEasy;
	public int correctElementEasy; //nr of index of answersEasy which is the correct answer
	public string[] answersHard;
	public int correctElementHard; //nr of index of answersHard which is the correct answer

	public Button[] answerButtons;

	public bool easy; //if true then player gets easy question
	public int mininumValue; //only if the value of the bought close exceeds (or is equal to) this the player only gets an easy question

	protected bool isBusy;
	protected bool buttonsAreEnabled;
	protected bool skipText;
	protected bool isBusyReplying;

    protected SceneController sceneConScript;
    protected PopupController popupScript;

	protected GameMaster gmScript;
	protected string currentLevel;


	//@Stefan maybe add a "repeat the question" option

	// Use this for initialization
	void Start() {
		currentLevel = SceneManager.GetActiveScene().name;
		gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

		if (gmScript.currentValue >= mininumValue) {
			easy = true;
		} else {
			easy = false;
		}

		//adds the question to the sentences
		newSentences = new string[sentences.Length+1];
		for (int i = 0; i < sentences.Length; i++) {
			newSentences [i] = sentences [i];
		}
		Start2 ();

		sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
		popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
	}

	void Start2(){
		isBusyReplying = false;
		skipText = false;
		isBusy = false;
		buttonsAreEnabled = false;

		if (easy) {
			newSentences [newSentences.Length - 1] = easyQuestion;
		} else {
			newSentences [newSentences.Length - 1] = hardQuestion;
		}

		for (int i = 0; i < answerButtons.Length; i++) {
			AddListener (answerButtons [i], i);
			if (easy) {
				answerButtons [i].GetComponentInChildren<Text> ().text = answersEasy [i]; 
			} else {
				answerButtons [i].GetComponentInChildren<Text> ().text = answersHard [i]; 
			}
			answerButtons [i].gameObject.SetActive (false);//hide the buttons at first
		}
	}


	void OnEnable() {
		StartCoroutine (TypeSentence (newSentences));
	}

	void Update () {
		if (sceneConScript == null || popupScript == null) {
			sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
			popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
		}

		/*if (Input.GetMouseButtonDown(0) && isBusy) {
			skipText = true;
		}*/
		if (Input.touchCount>0 && isBusy && !isBusyReplying) {
			skipText = true;
		}

	}
		

	protected void ButtonClick(int nrOfButton)
	{
		if ((easy && nrOfButton == correctElementEasy) || (!easy && nrOfButton == correctElementHard)) {
			//popupText.text = "THAT's CORRECT!!";
			skipText = false;
            StartCoroutine(AnswerRight());

		} else {
			//popupText.text = "Wrong...";
			skipText = false;
            StartCoroutine(AnswerWrong());
		}	
	}

	protected void AddListener(Button b, int i) //I need to use a method instead because else the value isn't "kept". For more info visit : http://answers.unity3d.com/questions/791573/46-ui-how-to-apply-onclick-handler-for-button-gene.html
	{
		b.onClick.AddListener (() => ButtonClick(i));
	}

    protected IEnumerator AnswerRight()
    {
		isBusyReplying = true;
		string [] temp = {"Dat klopt!"}; //deliberatly didn't choose "you may pass now, in case it asks again another question
		StartCoroutine (TypeSentence (temp));
		while (isBusy) {
			yield return new WaitForSeconds(1);
		}
		isBusyReplying = false;
		if (!easy) { //asks another question
			easy = true;
			newSentences = new string[1];
			Start2 ();
			OnEnable ();
		} else {
			popupScript.WinPopup();
			sceneConScript.SendCurrentLevel(currentLevel, true);
		}
    }

    protected IEnumerator AnswerWrong()
    {
		isBusyReplying = true;
		string [] temp = {"Dat klopt niet! Jij bent de alien!"};
		StartCoroutine (TypeSentence (temp));
		while (isBusy) { //this waits for the popup to be done speaking
			yield return new WaitForSeconds(1);
		}
		isBusyReplying = false;
        popupScript.GameOverPopUpDeath();
    }

	protected IEnumerator TypeSentence(string[] sentencesArray)
	{
		isBusy = true;
		for (int j = 0; j < sentencesArray.Length; j++) {
			for (int i = 0; i <= sentencesArray [j].Length; i++) {
				yield return new WaitForSeconds (typeSpeed);
				if (skipText) {
					popupText.text = sentencesArray [j];
					skipText = false;
					break;
				}
				popupText.text = sentencesArray [j].Substring (0, i);
			}
			yield return new WaitForSeconds (typeSpeed * 3); //this makes it wait a little longer at the end of each sentence
		}
		isBusy = false;
		if (!buttonsAreEnabled) {
			enableButtons ();
		}
	}

	protected void enableButtons()
	{
		foreach (Button b in answerButtons) { 
			b.gameObject.SetActive (true);
		}
		buttonsAreEnabled = true;
	}

}
