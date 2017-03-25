using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour {
    public enum CurrentLevel
    {
        MAS, ChinaTown, Kathedraal
    };
    public CurrentLevel currentLevel;
    PopupController popupConScript;
    SceneController sceneConScript;
    LevelController levelConScript;

    // Use this for initialization
    void Start () {
        levelConScript = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        popupConScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        levelConScript.finishedLevel = true;
        sceneConScript.SendCurrentLevel(currentLevel);
        popupConScript.WinPopup();
    }
}
