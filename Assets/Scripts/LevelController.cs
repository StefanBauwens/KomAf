using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public LevelPoint[] levels;
    protected Image[] levelImages;
    public Sprite unlockSprite;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CheckLevelUnlocked()
    {
        for(int i = 1; i <= levels.Length; i++)
        {
            if (levels[i].levelUnlocked)
            {
                levels[i].GetComponent<Image>().sprite = unlockSprite;
                levels[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void UnlockNextLevel(LevelPoint.NextLevel nextLevelToUnlock)
    {
        for (int i = 1; i <= levels.Length; i++)
        {
            if (nextLevelToUnlock.ToString() == levels[i].currentLevel.ToString())
            {
                levels[i].levelUnlocked = true;
            }
        }
    }

}
