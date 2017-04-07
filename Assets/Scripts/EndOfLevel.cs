using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour {

    private string currentLevel;
    private PopupController popupConScript;
    private SceneController sceneConScript;
    private LevelController levelConScript;
    private GameMaster gmScript;

    // Use this for initialization
    void Start () {
        currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log("currentlevel: " + currentLevel);
        levelConScript = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        popupConScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sceneConScript.SendCurrentLevel(currentLevel, true);
            gmScript.SaveProgress(currentLevel);
            gmScript.UpdateWinCoinText();
            popupConScript.WinPopup();
        }
    }
}
