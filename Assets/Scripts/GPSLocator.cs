﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SingleShadePlugin; //Using this plugin because of unity bug with location
//This means that everywhere where normally "Input.location" is used it's replaced by "InputLocation"
using UnityEngine.UI;

public class GPSLocator : MonoBehaviour {

	//public GPSLocator Instance{ get; set; }

	public float longitude;
	public float latitude;

	public bool isBusy;// = true;

	public Text gpsText;
	public Text debugger;

	// Use this for initialization
	public void Start () {
		//Instance = this;
		//DontDestroyOnLoad (this.transform.parent.gameObject);
		StartCoroutine (StartLocationService ());
		isBusy = true;
	}

	void Update () {
		if (!isBusy) {
			//Start ();
			StartCoroutine (StartLocationService ());
		}
		gpsText.text = "Lon:" + longitude.ToString () + " Lat:" + latitude.ToString ();
	}

	public IEnumerator StartLocationService ()
	{
		if (!Input.location.isEnabledByUser) {
			//USER HAS NOT enabled location services. Show popup asking to use it?
			debugger.text = "not enabled";
			isBusy = false;
			yield break;
		}


		Input.location.Start ();//(0.1f,0.1f);
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds (1);
			maxWait--;
			debugger.text = "is busy";
			isBusy = true;
		}

		if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed ) {
			//TIMED OUT SHOW POPUP Saying location was timed out or something went wrong
			isBusy = false;
			debugger.text = "failed";

			yield break;
		}
		longitude = Input.location.lastData.longitude;
		latitude = Input.location.lastData.latitude;
		debugger.text = "lonlat set";

		isBusy = false;
		yield break;
	}


}
