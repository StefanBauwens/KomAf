using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    protected SceneController sceneConScript;
    protected Settings settingsScript;
    public Button menuButton;
    public Button restartButton;
    public Button settingsButton;

    // Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        settingsButton.onClick.AddListener(() => settingsScript.OpenSettings());
        menuButton.onClick.AddListener(() => sceneConScript.LoadAntwerpMap());
        restartButton.onClick.AddListener(() => sceneConScript.RestartLevel());
    }

    
}
