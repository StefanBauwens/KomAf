using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GATE15 : MonoBehaviour {

    public CanvasGroup gate15canvasGroup;
    public GameObject gate15Popup;
    public GameObject infoOverSpelPopup;
    public GameObject verhaalPopup;
    protected AudioSource audSource;
    protected Settings settingsScript;

	// Use this for initialization
	void Start () {
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        audSource = GetComponent<AudioSource>();
	}
	
    public void OpenGate15()
    {
        settingsScript.PlayButtonSound(audSource);
        gate15canvasGroup.alpha = 1;
        gate15canvasGroup.interactable = true;
        gate15canvasGroup.blocksRaycasts = true;
    }

    public void ExitGate15()
    {
        settingsScript.PlayButtonSound(audSource);
        gate15Popup.SetActive(true);
        gate15canvasGroup.alpha = 0;
        gate15canvasGroup.interactable = false;
        gate15canvasGroup.blocksRaycasts = false;
    }

    public void OpenInfoOverSpelPopup()
    {
        settingsScript.PlayButtonSound(audSource);
        gate15Popup.SetActive(false);
        infoOverSpelPopup.SetActive(true);
    }

    public void ExitInfoOverSpelPopup()
    {
        settingsScript.PlayButtonSound(audSource);
        infoOverSpelPopup.SetActive(false);
        ExitGate15();
    }

    public void OpenVerhaalPopup()
    {
        settingsScript.PlayButtonSound(audSource);
        gate15Popup.SetActive(false);
        verhaalPopup.SetActive(true);
    }

    public void ExitVerhaalPopup()
    {
        settingsScript.PlayButtonSound(audSource);
        verhaalPopup.SetActive(false);
        ExitGate15();
    }

}
