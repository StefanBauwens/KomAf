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

    protected SceneController sceneConScript;
    protected PopupController popupScript;

	//@Stefan maybe add a "repeat the question" option

	// Use this for initialization
	void Start() {
		isBusy = false;

		//adds the question to the sentences
		Array.Resize (ref sentences, sentences.Length + 1); 
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
		sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
		popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
	}

	void OnEnable() {
		StartCoroutine ("StartWriting"); 
	}

	void Update () {
		if (sceneConScript == null || popupScript == null) {
			sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
			popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
		}
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
		Debug.Log ("Coroutine started");
		for (int j = 0; j < sentences.Length; j++) {
			for (int i = 0; i <= sentences[j].Length; i++) {
				yield return new WaitForSeconds(typeSpeed);
				popupText.text = sentences [j].Substring (0, i);
			}
			yield return new WaitForSeconds(typeSpeed*3); //this makes it wait a little longer at the end of each sentence
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
            StartCoroutine(AnswerRight());
            

		} else {
			popupText.text = "Wrong...";
            StartCoroutine(AnswerWrong());
		}	
	}

	protected void AddListener(Button b, int i) //I need to use a method instead because else the value isn't "kept". For more info visit : http://answers.unity3d.com/questions/791573/46-ui-how-to-apply-onclick-handler-for-button-gene.html
	{
		b.onClick.AddListener (() => ButtonClick(i));
	}

    protected IEnumerator AnswerRight()
    {
        popupText.text = "You may pass now...";
        yield return new WaitForSeconds(1.5f);
        popupScript.WinPopup();
    }

    protected IEnumerator AnswerWrong()
    {
        popupText.text = "That's too suspicious, you are the alien!";
        yield return new WaitForSeconds(1.5f);
        popupScript.GameOverPopUpDeath();
    }

}
