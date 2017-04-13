using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSTest : MonoBehaviour {

	public Text gpsText;
	
	// Update is called once per frame
	void Update () {
		if (GPSLocator.Instance.isBusy == false) {
			GPSLocator.Instance.RunIt();
		}
		gpsText.text = "Lon:" + GPSLocator.Instance.longitude.ToString () + " Lat:" + GPSLocator.Instance.latitude.ToString ();
	}
}
