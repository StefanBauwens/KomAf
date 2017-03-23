using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool nextLevelUnlocked;
    protected int maxHighscore;
    LevelController levelConScript;

	// Use this for initialization
	void Start () {
        levelConScript = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PageFoundLevel(Page.LevelOfPage levelOfPage)
    {
        if(currentLevel.ToString() == levelOfPage.ToString())
        {
            nextLevelUnlocked = true;
            levelConScript.UnlockNextLevel(nextLevel);
        }
    }


}
