using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPoint : StopPoint {
    public enum CurrentLevel
    {
        MAS, ChinaTown, Kathedraal
    };
    public enum NextLevel
    {
        MAS, ChinaTown, Kathedraal
    };
    public CurrentLevel currentLevel;
    public NextLevel nextLevel;
    public bool levelUnlocked;
    public short levelACount;
    Button levelButton;
    SceneController sceneConScript;

	// Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();

        levelButton = GetComponent<Button>(); 
        levelButton.onClick.AddListener(() => sceneConScript.OpenLocationPopup(gameObject.name));
    }













}
