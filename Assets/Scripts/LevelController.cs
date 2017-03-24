using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public LevelPoint[] levels;
    protected Image[] levelImages;
    public Sprite unlockSprite;
    public int levelScore;
    public int levelHighScore;
    GameMaster gmScript;
    public bool playingLevel;

    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);    
    //}

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    //public void CheckLevelUnlocked()
    //{
    //    for(int i = 1; i <= levels.Length; i++)
    //    {
    //        if(PlayerPrefs.GetString(levels[i].currentLevel.ToString(), "locked") == "unlocked")
    //        {
    //            levels[i].levelUnlocked = true;
    //            levels[i].GetComponent<Image>().sprite = unlockSprite;
    //            levels[i].GetComponent<Button>().interactable = true;
    //        }   
    //    }
    //}

    //public void UnlockNextLevel(LevelPoint.NextLevel nextLevelToUnlock)
    //{
    //    for (int i = 1; i <= levels.Length; i++)
    //    {
    //        if (nextLevelToUnlock.ToString() == levels[i].currentLevel.ToString())
    //        {
    //            levels[i].levelUnlocked = true;
    //            gmScript.SaveUnlockedLevel(levels[i].currentLevel);
    //        }
    //    }
    //}

    //public void PageFoundLevel(Page.LevelOfPage levelOfPage)
    //{
    //    for (int i = 1; i <= levels.Length; i++)
    //    {
    //        if(levelOfPage.ToString() == levels[i].currentLevel.ToString())
    //        {
    //            levels[i].nextLevelUnlocked = true;
    //            UnlockNextLevel(levels[i].nextLevel);
    //        }
    //    }
    //}


}
