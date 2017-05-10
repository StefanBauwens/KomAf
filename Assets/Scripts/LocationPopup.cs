﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPopup : MonoBehaviour {

    public string locationName;
    public Text locationInfoText;
    public Text locationNameText;
    public Text coinsCollectedText;
    public Text maxCoins;
    private CanvasGroup locationPopupCanvas;
    private Button resumeButton;
    private SceneController sceneConScript;
    private GameMaster gmScript;
    private LocationInfoKeeper locationInfoScript;

	// Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        locationInfoScript = GameObject.FindGameObjectWithTag("LocationInfoKeeper").GetComponent<LocationInfoKeeper>();
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(() => sceneConScript.LoadLevelByName(locationName)); // register button event and pass parameter with lambda
        locationPopupCanvas = GetComponentInParent<CanvasGroup>();
    }

    public void CheckLocationInfo()
    {
        if(locationName == "SintAnnastrand")
        {
            locationInfoText.text = locationInfoScript.sintannastrandText.text;
        }
        else if(gmScript.pageCount >= 1 && locationName == "ZwemvijverBoekenberg")
        {
            locationInfoText.text = locationInfoScript.zwemvijverboekenbergText.text;
        }
        else if (gmScript.pageCount >= 2 && locationName == "Vlaeykensgang")
        {
            locationInfoText.text = locationInfoScript.vlaeykensgangText.text;
        }
        else if(gmScript.pageCount >= 3 && locationName == "ChinaTown")
        {
            locationInfoText.text = locationInfoScript.chinatownText.text;
        }
        else if (gmScript.pageCount >= 4 && locationName == "DeRuien")
        {
            locationInfoText.text = locationInfoScript.deruienText.text;
        }
        else if (gmScript.pageCount >= 5 && locationName == "ErfgoedBibliotheek")
        {
            locationInfoText.text = locationInfoScript.erfgoedbibliotheekText.text;
        }
        else if (gmScript.pageCount >= 6 && locationName == "Kammenstraat")
        {
            locationInfoText.text = locationInfoScript.kammenstraatText.text;
        }
        else
        {
            locationInfoText.text = "Pagina van GATE15 brochure nog niet gevonden.";
        }
    }

    public void CloseLocationPopup()
    {
        locationPopupCanvas.alpha = 0;
        locationPopupCanvas.interactable = false;
        locationPopupCanvas.blocksRaycasts = false;
    }
}
