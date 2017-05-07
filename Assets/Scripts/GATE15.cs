using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GATE15 : MonoBehaviour {

    public CanvasGroup gate15canvasGroup;
    public GameObject gate15Popup;
    public GameObject infoOverSpelPopup;
    public GameObject verhaalPopup;

	// Use this for initialization
	void Start () {
	}
	
    public void OpenGate15()
    {
        gate15canvasGroup.alpha = 1;
        gate15canvasGroup.interactable = true;
        gate15canvasGroup.blocksRaycasts = true;
    }

    public void ExitGate15()
    {
        gate15Popup.SetActive(true);
        gate15canvasGroup.alpha = 0;
        gate15canvasGroup.interactable = false;
        gate15canvasGroup.blocksRaycasts = false;
    }

    public void OpenInfoOverSpelPopup()
    {
        gate15Popup.SetActive(false);
        infoOverSpelPopup.SetActive(true);
    }

    public void ExitInfoOverSpelPopup()
    {
        infoOverSpelPopup.SetActive(false);
        ExitGate15();
    }

    public void OpenVerhaalPopup()
    {
        gate15Popup.SetActive(false);
        verhaalPopup.SetActive(true);
    }

    public void ExitVerhaalPopup()
    {
        verhaalPopup.SetActive(false);
        ExitGate15();
    }

}
