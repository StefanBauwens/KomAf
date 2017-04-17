using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSTest : MonoBehaviour {

	public Text gpsText;
	public GPSLocator gps;
	
	// Update is called once per frame

	/*void Start()
	{
		gps.isBusy = false;
		gps.StartCoroutine (gps.StartLocationService ());
	}

	void Update () {
		if (gps.isBusy == false) {
			gps.Start ();
		}
		gpsText.text = "Lon:" + gps.longitude.ToString () + " Lat:" + gps.latitude.ToString ();
	}*/
}
