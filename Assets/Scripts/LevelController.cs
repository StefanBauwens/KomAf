using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private static LevelController instanceRef;
    protected Image[] levelImages;
    public Sprite unlockSprite;
	public Sprite lockSprite;

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
        }
    }

    public void CheckLevelUnlocked()
    {
        for (int i = 1; i < levels.Length; i++) // index = 1 --> Sint-Annastrand always unlocked
        {      
			if (PlayerPrefs.GetString ("locked/unlocked" + levels [i].ToString (), "locked") == "unlocked") {
				levels [i].levelUnlocked = true;
				MakeLevelButtonInteractable (levels [i]);
			} else {
				levels [i].levelUnlocked = false;	
				MakeLevelNotInteractable (levels [i]);
			}
        }
    }

    public void UnlockNextLevel(LevelPoint.NextLevel nextLevelToUnlock)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (nextLevelToUnlock.ToString() == levels[i].currentLevel.ToString())
            {
                if (!levels[i].levelUnlocked)
                {
                    levels[i].levelUnlocked = true;

                    MakeLevelButtonInteractable(levels[i]);
                    gmScript.SaveUnlockedLevel(levels[i]);
                }
            }
        }
    }

    public void GetLevelUnlocker(string currentLevel)
    {
        for (int i = 0; i < levels.Length; i++)
        { 
            if (currentLevel == levels[i].currentLevel.ToString())
            {
                UnlockNextLevel(levels[i].nextLevel);       
            }
        }
    }

	public void UnlockAllLevels()
	{
		for (int i = 0; i < levels.Length; i++) {
			UnlockNextLevel (levels [i].nextLevel);
		}
	}

    public void MakeLevelButtonInteractable(LevelPoint level)
    {
		if (level.GetComponent<Image> () != null) {
			level.GetComponent<Image> ().sprite = unlockSprite;
		} else {
			level.GetComponent<SpriteRenderer>().sprite = unlockSprite;
		}
        //level.GetComponent<Button>().interactable = true;
    }

	public void MakeLevelNotInteractable(LevelPoint level)
	{
		if (level.GetComponent<Image> () != null) {
			level.GetComponent<Image> ().sprite = lockSprite;
		} else {
			level.GetComponent<SpriteRenderer>().sprite = lockSprite;
		}
	}

    public void SetLevelsFromArray()
    {
        levelKeeper = GameObject.FindGameObjectWithTag("LevelKeeper").GetComponent<LevelKeeper>();
        levels = levelKeeper.levels;
    }
}
