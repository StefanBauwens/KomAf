using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPopup : MonoBehaviour {

    public string locationName;
    //public Text infoText;
    public Text locationText;
    public Text coinsCollectedText;
    Button resumeButton;
    SceneController sceneConScript;

	// Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();

        resumeButton = GetComponentInChildren<Button>();
        resumeButton.onClick.AddListener(() => sceneConScript.LoadLevelByName(locationName)); // register button event and pass parameter with lambda
    }


}
