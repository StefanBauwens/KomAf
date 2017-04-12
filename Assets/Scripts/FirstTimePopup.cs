using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimePopup : MonoBehaviour {

	public Button closeBtn;

	// Use this for initialization
	void Start () {
		closeBtn.onClick.AddListener(ClosePanel);
		if (PlayerPrefs.GetInt ("seenFirstPopup", 0) == 1) {
			ClosePanel ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ClosePanel()
	{
		PlayerPrefs.SetInt ("seenFirstPopup", 1);
		PlayerPrefs.Save ();
		this.gameObject.SetActive (false);
	}
}
