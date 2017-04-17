using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    private SceneController sceneConScript;
    public Button menuButton;
    public Button restartButton;

    // Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();

        menuButton.onClick.AddListener(() => sceneConScript.LoadAntwerpMap());
        restartButton.onClick.AddListener(() => sceneConScript.RestartLevel());
    }

    
}
