using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private static LevelController instanceRef;
    protected Image[] levelImages;
    public Sprite unlockSprite;
    public int levelScore;
    public int levelHighScore;
    GameMaster gmScript;
    public bool playingLevel;
    LevelKeeper levelKeeper;
    public LevelPoint[] levels;
    public bool finishedLevel;

    void Awake()
    {
        // prevent duplicates in scenes
        if(instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    //public void CheckLevelUnlocked()
    //{
    //    for (int i = 1; i <= levels.Length; i++)
    //    {
    //        if (PlayerPrefs.GetString(levels[i].currentLevel.ToString(), "locked") == "unlocked")
    //        {
    //            levels[i].levelUnlocked = true;
    //            levels[i].GetComponent<Image>().sprite = unlockSprite;
    //            levels[i].GetComponent<Button>().interactable = true;
    //        }
    //    }
    //}

    public void UnlockNextLevel(LevelPoint.NextLevel nextLevelToUnlock)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (nextLevelToUnlock.ToString() == levels[i].currentLevel.ToString())
            {
                //gmScript.SaveUnlockedLevel(levels[i].currentLevel);
                
                    levels[i].levelUnlocked = true;
                
                    levels[i].GetComponent<Image>().sprite = unlockSprite;
                    levels[i].GetComponent<Button>().interactable = true;         
            }
        }
    }

    public void GetLevelUnlocker(EndOfLevel.CurrentLevel currentLevel)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (currentLevel.ToString() == levels[i].currentLevel.ToString())
            {
                UnlockNextLevel(levels[i].nextLevel);
            }
        }
    }
    //  FUNCTION TO CHECK SCENECHANGE ONLY 1 IN ALL SCRIPTS!!! GODVERDOMME

    //void OnEnable()
    //{
    //    SceneManager.activeSceneChanged += LevelFinishedLoading; // satrt listening for scene change when script enabled
    //}

    //void OnDisable()
    //{
    //    // Remember to always have an unsubscription for every delegate you subscribe to!
    //    SceneManager.activeSceneChanged -= LevelFinishedLoading; // stop listening for scene change when script disabled
    //}

    //void LevelFinishedLoading(Scene previousScene, Scene activeScene)
    //{
    //    //voert maar 1x uit...
    //    if (activeScene.name == "AntwerpMap")
    //    {
    //        Debug.Log("in levelcontroller: " + count);
    //        levelKeeper = GameObject.FindGameObjectWithTag("LevelKeeper").GetComponent<LevelKeeper>();
    //        levels = levelKeeper.levels;
    //        Debug.Log("assigned levels from levelkeeper");
    //    }
    //    else if (activeScene.name != "AntwerpMap")
    //    {
    //        for(int i = 0; i < levels.Length; i++)
    //        {
    //            levels[i] = null;
    //        }        
    //    }     
    //}

    public void SetLevelsFromArray()
    {
        levelKeeper = GameObject.FindGameObjectWithTag("LevelKeeper").GetComponent<LevelKeeper>();
        levels = levelKeeper.levels;
    }

    public void SetLevelsAsNull()
    {
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i] = null;
        }
        
    }
}
