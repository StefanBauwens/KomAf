using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
	public Image Timer;
	public int seconds;
	protected float timeToDelay;
	protected float waitTime;

	void Start()
	{
		waitTime = 0.1f;
		timeToDelay = waitTime / seconds;
		StartCoroutine ("CountDown");
	}

	IEnumerator CountDown()
	{
		while (Timer.fillAmount >= 0f) {
			yield return new WaitForSeconds (waitTime);
			Timer.fillAmount -= timeToDelay;
		}

	}
}