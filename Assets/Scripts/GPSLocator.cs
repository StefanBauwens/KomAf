using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SingleShadePlugin; //Using this plugin because of unity bug with location
//This means that everywhere where normally "Input.location" is used it's replaced by "InputLocation"
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GPSLocator : MonoBehaviour {

	//public GPSLocator Instance{ get; set; }

	public float longitude;
	public float latitude;

	//protected float fakelat = 51.166498f;
	//protected float fakelon = 4.132195f;

	public bool isBusy;// = true;

	public Text gpsText;

	public string[] Levels;
	public Vector2[] LevelsLatLong; //Array Levels and this array should be same long
	public float kmToleranceToLevel = 0.05f; //50 meter

	//public Text debugger;

	// Use this for initialization
	public void Start () {
		//Instance = this;
		//DontDestroyOnLoad (this.transform.parent.gameObject);
		StartCoroutine (StartLocationService ());
		isBusy = true;
		Time.timeScale = 1; //this is what caused the problem!!
	}

	void Update () {
		if (!isBusy) {
			StartCoroutine (StartLocationService ());
		}
		//gpsText.text = "Lon:" + longitude.ToString () + " Lat:" + latitude.ToString ();
		//gpsText.text = "Distance to fakelatlon =" + getDistanceFromLatLonInKm(latitude, longitude, fakelat, fakelon).ToString();

		//Iterate over the latitudes & longitudes to see if player is in the neighborhood of the location
		for (int i = 0; i < LevelsLatLong.Length; i++) {
			if (getDistanceFromLatLonInKm(LevelsLatLong[i].x, LevelsLatLong[i].y, latitude, longitude) <= kmToleranceToLevel) {
				if (PlayerPrefs.GetInt(Levels [i] + "_secret", 0)==0) { //this makes that it only shows the message the first time.
					gpsText.text = Levels [i] + " secret unlocked!"; 
					//REPLACE THE ABOVE LINE WITH A NICE POPUP THAT SAYS YOU UNLOCKED SECET CONTENT IN THE SPECIFIC LEVEL
				}
				PlayerPrefs.SetInt (Levels [i] + "_secret", 1); //1 is secret unlocked, 0 means it's still locked
				PlayerPrefs.Save ();
			}
			if (!PlayerPrefs.HasKey(Levels[i] + "_secret")) { //if playerprefs don't exist yet create it with value 0
				PlayerPrefs.SetInt(Levels[i] + "_secret",  0);
				PlayerPrefs.Save();
			}
		}
	}

	public IEnumerator StartLocationService ()
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
			isBusy = false;
			yield break;
		}
		longitude = Input.location.lastData.longitude;
		latitude = Input.location.lastData.latitude;
		isBusy = false;
		yield break;
	}


	float getDistanceFromLatLonInKm(float lat1,float lon1,float lat2, float lon2) {
		int R = 6371; //Radius of the earth in km
		float dLat = deg2rad(lat2-lat1);
		float dLon = deg2rad(lon2-lon1);
		float a =
			(float)Math.Sin(dLat/2) * (float)Math.Sin(dLat/2) +
			(float)Math.Cos(deg2rad(lat1)) * (float)Math.Cos(deg2rad(lat2)) *
			(float)Math.Sin(dLon/2) * (float)Math.Sin(dLon/2);

		float c = 2 * (float)Math.Atan2(Math.Sqrt(a), (float)Math.Sqrt(1-a));
		float d = R * c; //distance in km
		return d;
	}
		
	float deg2rad(float deg)
	{
		return deg * (float)(Math.PI/180f);
	}
}
