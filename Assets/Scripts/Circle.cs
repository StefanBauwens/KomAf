using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
	public Image Timer;
	public int seconds;
	protected float timeToDelay;
	protected float waitTime;

	protected GameObject player;
	//protected bool restart; //if restart is true then it meanse player hit another "jumphigher" obect and timer has to start again
	RectTransform myRectTransform;
	//myRectTransform.localPosition += Vector3.right;

	void Start()
	{
		//restart = false;
		waitTime = 0.1f;
		timeToDelay = waitTime / seconds;
		player = GameObject.FindGameObjectWithTag ("Player"); //gets the player
		myRectTransform = GetComponent<RectTransform>();
		Timer.fillAmount = 0;
		this.gameObject.GetComponent<Image> ().enabled = false;
		//StartTimer ();
	}

	void Update()
	{
		myRectTransform.localPosition = Camera.main.WorldToScreenPoint (player.transform.position) - new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2.5f, 0);

		//this.gameObject.transform.position = player.transform.position;
	}

	IEnumerator CountDown()
	{
		while (Timer.fillAmount >= 0f) {
			yield return new WaitForSeconds (waitTime);
			Timer.fillAmount -= timeToDelay;
			if (Timer.fillAmount == 0) {
				this.gameObject.GetComponent<Image> ().enabled = false;
			}
		}
	}

	public void StartTimer ()
	{
		this.gameObject.GetComponent<Image> ().enabled = true;
		Timer.fillAmount = 1;
		//restart = true;
		StopCoroutine("CountDown");
		StartCoroutine ("CountDown");
	}
}