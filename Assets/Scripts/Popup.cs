using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour {
	protected GameObject thisCanvas;

	// Use this for initialization
	void Start () {
		thisCanvas = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close()
	{
		thisCanvas.GetComponent<CanvasGroup> ().alpha = 0f;
		thisCanvas.GetComponent<CanvasGroup> ().interactable = false;
		thisCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
