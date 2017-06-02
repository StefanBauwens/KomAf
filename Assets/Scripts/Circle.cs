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
	RectTransform myRectTransform;
	//protected Vector3 originalRectPos;

	void Start()
	{
		waitTime = 0.1f;
		timeToDelay = waitTime / seconds;
		player = GameObject.FindGameObjectWithTag ("Player"); //gets the player
		myRectTransform = GetComponent<RectTransform>();
		Timer.fillAmount = 0;
		this.gameObject.GetComponent<Image> ().enabled = false;
		//originalRectPos = myRectTransform.localPosition;
	}

	void Update()
	{
		myRectTransform.localPosition = Camera.main.WorldToScreenPoint (player.transform.position) - new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/3f, 0);
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
		StopCoroutine("CountDown");
		StartCoroutine ("CountDown");
	}
}