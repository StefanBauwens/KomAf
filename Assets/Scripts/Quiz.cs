using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour {

	public Text popupText;
	public string[] sentences; 
	public float typeSpeed; // I suggest using something like 0.07

	public string easyQuestion;
	public string hardQuestion;

	public string[] answersEasy;
	public int correctElementEasy; //nr of index of answersEasy which is the correct answer
	public string[] answersHard;
	public int correctElementHard; //nr of index of answersHard which is the correct answer

	public Button[] answerButtons;

	public bool easy; //if true then player gets easy question

	protected bool isBusy;

	//@Stefan maybe add a "repeat the question" option

	// Use this for initialization
	void Start () {
		isBusy = false;

		//adds the question to the sentences
		Array.Resize(ref sentences, sentences.Length + 1); 
		if (easy) {
			sentences [sentences.Length - 1] = easyQuestion;
		} else {
			sentences [sentences.Length - 1] = hardQuestion;
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

		StartCoroutine ("StartWriting"); 
	}

	/*protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player" && isBusy == false) {
			isBusy = true;
			StartCoroutine ("StartWriting");
		}
	}*/

	protected IEnumerator StartWriting()
	{
		for (int j = 0; j < sentences.Length; j++) {
			for (int i = 0; i <= sentences[j].Length; i++) {
				yield return new WaitForSeconds(typeSpeed);
				popupText.text = sentences [j].Substring (0, i);
			}
			yield return new WaitForSeconds(typeSpeed*2); //this makes it wait a little longer at the end of each sentence
		}

		foreach (Button b in answerButtons) { //Show the buttons only after asking the question
			b.gameObject.SetActive (true);
		}
		isBusy = false;
	}

	protected void ButtonClick(int nrOfButton)
	{
		if ((easy && nrOfButton == correctElementEasy) || (!easy && nrOfButton == correctElementHard)) {
			popupText.text = "THAT's CORRECT!!";
			//proceed to next level @CINDY DO YOUR MAGIC HERE
		} else {
			popupText.text = "Wrong...";
			//Game over thingie @CINDY HERE TOO
		}	
	}

	protected void AddListener(Button b, int i) //I need to use a method instead because else the value isn't "kept". For more info visit : http://answers.unity3d.com/questions/791573/46-ui-how-to-apply-onclick-handler-for-button-gene.html
	{
		b.onClick.AddListener (() => ButtonClick(i));
	}


}
