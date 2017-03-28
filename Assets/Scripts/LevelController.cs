using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private static LevelController instanceRef;
    protected Image[] levelImages;
    public Sprite unlockSprite;

    public GameMaster gmScript;
    private LevelKeeper levelKeeper;
    public LevelPoint[] levels;

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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("deleted playerprefs");
            Debug.Log("deleted playerprefs");
        }
    }

    public void CheckLevelUnlocked()
    {
        for (int i = 1; i < levels.Length; i++) // index = 1 --> MAS always unlocked
        {      
            if (PlayerPrefs.GetString("locked/unlocked" + levels[i].ToString(), "locked") == "unlocked")
            {
                levels[i].levelUnlocked = true;
                MakeLevelButtonInteractable(levels[i]);
            }
        }
    }

    public void UnlockNextLevel(LevelPoint.NextLevel nextLevelToUnlock)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (nextLevelToUnlock.ToString() == levels[i].currentLevel.ToString())
            {
                if (levels[i].levelUnlocked)
                {
                    Debug.Log("Already saved");
                }
                else
                {
                    levels[i].levelUnlocked = true;

                    if (levels[i].levelUnlocked)
                    {
                        MakeLevelButtonInteractable(levels[i]);
                        gmScript.SaveUnlockedLevel(levels[i]);
                    }
                }    
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

    public void MakeLevelButtonInteractable(LevelPoint level)
    {
        level.GetComponent<Image>().sprite = unlockSprite;
        level.GetComponent<Button>().interactable = true;
    }

    public void SetLevelsFromArray()
    {
        
        levelKeeper = GameObject.FindGameObjectWithTag("LevelKeeper").GetComponent<LevelKeeper>();
        levels = levelKeeper.levels;
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
}
