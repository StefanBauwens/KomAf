using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimePopup : MonoBehaviour {

	//THIS SCRIPT IS OBSOLETE

	public Button closeBtn;
    public CanvasGroup firstTimeCanvas;
	// Use this for initialization
	void Start () {
        firstTimeCanvas = GameObject.FindGameObjectWithTag("FirstTimeCanvas").GetComponent<CanvasGroup>();
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
        firstTimeCanvas.alpha = 0;
        firstTimeCanvas.interactable = false;
        firstTimeCanvas.blocksRaycasts = false;
	}
}
