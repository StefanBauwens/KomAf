using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    private static GameMaster instanceRef;
    public int coinsCollectedInLevel;
    public short totalPageCount;
    //public short totalACount;
    public bool playingLevel;
    protected string tempLevel;

    Text coinText;
    //public Text pauseHighScore;
    //public Text gameOverHighscore;
    public Text winCoinText;
    WinGame winScript;
    Page pageScript;
    public LevelController levelConScript;
    

    void Awake()
    {
        if(instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        //levelConScript.CheckLevelUnlocked();
        //GetSavedHighScore();
        //GetSavedTotalPageCount();
        //GetSavedTotalACount();
    }
    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        //UpdateHighScoreText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playingLevel)
        {
            
            coinText.text = coinsCollectedInLevel.ToString();

        }
        Debug.Log(SceneManager.GetActiveScene().name);
	}

    public void UpdateWinCoinText()
    {
        winCoinText.text = coinsCollectedInLevel.ToString();
    }

    public void SaveCoins(string level)
    {
        //totalACount += winScript.CountAPoints();
        PlayerPrefs.SetInt("coinsCollectedInLevel"+ level, coinsCollectedInLevel);
    }

    public void SavePageCount()
    {
        PlayerPrefs.SetInt("totalPageCount", totalPageCount);
    }

    public void SaveUnlockedLevel(LevelPoint.CurrentLevel level)
    {
        for(int i = 0; i < levelConScript.levels.Length; i++)
        {
            if(levelConScript.levels[i].ToString() == level + " (LevelPoint)")  // add (LevelPoint)!
            {
                PlayerPrefs.SetString("locked/unlocked" + levelConScript.levels[i].ToString(), "unlocked");
            }
        }
    }

    public int GetCoinsCollectedInLevel(string level)
    {
        return PlayerPrefs.GetInt("coinsCollectedInLevel" + level, 0);
    }

    public void SetCoinsCollectedInLevel()
    {
        coinsCollectedInLevel = GetCoinsCollectedInLevel(SceneManager.GetActiveScene().name);
    }

    //public void SaveACount()
    //{
    //    PlayerPrefs.SetInt("totalACount", totalACount);
    //    PlayerPrefs.Save();
    //}

    //public void GetSavedTotalPageCount()
    //{
    //    totalPageCount = (short)PlayerPrefs.GetInt("totalPageCount", 0);
    //}

    //public void GetSavedTotalACount()
    //{
    //    totalACount = (short)PlayerPrefs.GetInt("totalACount", 0);
    //}

    //void UpdateHighScoreText()
    //{
    //    if(highScore > 0)
    //    {
    //        pauseHighScore.text = highScore.ToString();
    //        //gameOverHighscore.text.ToString();
    //    }
    //}

    public void UpdatePageCount(string levelOfPage)
    {
        totalPageCount += 1;
    }

    public void GetGameObjectsFromScene()
    {
        coinText = GameObject.Find("Canvas/CoinUI/CoinText").GetComponent<Text>();
        winCoinText = GameObject.Find("Canvas/WinCanvas/WinPopup/winCoinText").GetComponent<Text>();
        if (GameObject.Find("Page") != null)
        {
            pageScript = GameObject.Find("Page").GetComponent<Page>();
        }
        else
        {
            pageScript = null;
        }
    }


    public void SaveProgress(string currentLevel)
    {
        tempLevel = currentLevel;
        SaveCoins(tempLevel);
        SavePageCount();
        PlayerPrefs.Save();
    }




}
