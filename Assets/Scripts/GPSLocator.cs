using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SingleShadePlugin; //Using this plugin because of unity bug with location
//This means that everywhere where normally "Input.location" is used it's replaced by "InputLocation"

public class GPSLocator : MonoBehaviour {

	public static GPSLocator Instance{ get; set; }

	public float longitude = 0;
	public float latitude = 0;

	public bool isBusy = true;

	// Use this for initialization
	public void Start () {
		Instance = this;
		DontDestroyOnLoad (gameObject);
		StartCoroutine (StartLocationService ());
	}

	IEnumerator StartLocationService ()
	{
		if (!Input.location.isEnabledByUser) {
			//USER HAS NOT enabled location services. Show popup asking to use it?
			isBusy = false;
			yield break;
		}


		Input.location.Start ();//(0.1f,0.1f);
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds (1);
			maxWait--;
			isBusy = true;
		}

		if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed ) {
			//TIMED OUT SHOW POPUP Saying location was timed out or something went wrong
			Debug.Log("Something went wrong!");
			isBusy = false;
			yield break;
		}
		longitude = Input.location.lastData.longitude;
		latitude = Input.location.lastData.latitude;
		isBusy = false;

		yield break;
	}


}
