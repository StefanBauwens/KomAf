using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GPSLocator : MonoBehaviour {

	public float longitude;
	public float latitude;

	public bool isBusy;// = true;

    public GameObject gpsPopupCanvas;
    public Text levelText;

	public string[] Levels;
	public Vector2[] LevelsLatLong; //Array Levels and this array should be same long
	public float kmToleranceToLevel = 0.05f; //50 meter

	// Use this for initialization
	public void Start () {
		isBusy = true;
		Time.timeScale = 1; 
	}

	void Update () {
        if (!isBusy) {
			StartCoroutine (StartLocationService ());
		}
        
		//Iterate over the latitudes & longitudes to see if player is in the neighborhood of the location
		for (int i = 0; i < LevelsLatLong.Length; i++) {
			if (getDistanceFromLatLonInKm(LevelsLatLong[i].x, LevelsLatLong[i].y, latitude, longitude) <= kmToleranceToLevel) {
				if (PlayerPrefs.GetInt(Levels [i] + "_secret", 0)==0) { //this makes that it only shows the message the first time.
                    levelText.text = Levels[i];
                    OpenCloseGPSPopup();
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
			//USER HAS NOT enabled location services
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
			//TIMED OUT 
			isBusy = false;
			yield break;
		}
		longitude = Input.location.lastData.longitude;
		latitude = Input.location.lastData.latitude;
		isBusy = false;
		yield break;
	}

	public void enableFakeLocation()
	{
		latitude = LevelsLatLong [0].x;
		longitude = LevelsLatLong [0].y;
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

    public void OpenCloseGPSPopup()
    {
		gpsPopupCanvas.GetComponent<CanvasGroup> ().blocksRaycasts = !gpsPopupCanvas.GetComponent<CanvasGroup> ().blocksRaycasts;
        gpsPopupCanvas.GetComponent<EasyTween>().OpenCloseObjectAnimation();
    }

}
