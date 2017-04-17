using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPopup : MonoBehaviour {

    public string locationName;
    public Text locationInfoText;
    public Text locationNameText;
    public Text coinsCollectedText;
    public Text maxCoins;
    private Button resumeButton;
    private SceneController sceneConScript;
    private GameMaster gmScript;
    private LocationInfoKeeper locationInfoScript;

	// Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        locationInfoScript = GameObject.FindGameObjectWithTag("LocationInfoKeeper").GetComponent<LocationInfoKeeper>();
        resumeButton = GetComponentInChildren<Button>();
        resumeButton.onClick.AddListener(() => sceneConScript.LoadLevelByName(locationName)); // register button event and pass parameter with lambda
    }

    public void CheckLocationInfo()
    {
        if(locationName == "SintAnnastrand")
        {
            locationInfoText.text = locationInfoScript.sintAnnastrandText.text;
        }
        else if(gmScript.pageCount >= 1 && locationName == "ZwemvijverBoekenberg")
        {
            locationInfoText.text = locationInfoScript.zwemvijverBoekenbergText.text;
        }
        else if (gmScript.pageCount >= 2 && locationName == "Vlaeykensgang")
        {
            locationInfoText.text = locationInfoScript.vlaeykensgangText.text;
        }
        else
        {
            locationInfoText.text = "Pagina van GATE15 brochure nog niet gevonden.";
        }
    }
}
