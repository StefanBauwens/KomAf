using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    protected int highScore;
    public int score;
    public short totalPageCount;
    public short totalACount;
    public bool levelCompleted;
    public bool playingLevel;

    public Text scoreText;
    public Text pauseHighScore;
    public Text gameOverHighscore;
    public Text winScore;
    public WinGame winScript;
    public Page pageScript;
    public LevelController levelConScript;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
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
            scoreText.text = score.ToString();
        }
	}

    public void SaveScore()
    {
        winScore.text = score.ToString();
        
        totalACount += winScript.CountAPoints();

        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            highScore = score;
            UpdateHighScoreText();
        }
    }

    public void SavePageCount()
    {
        PlayerPrefs.SetInt("totalPageCount", totalPageCount);
        PlayerPrefs.Save();
    }

    public void SaveACount()
    {
        PlayerPrefs.SetInt("totalACount", totalACount);
        PlayerPrefs.Save();
    }

    public void SaveUnlockedLevel(LevelPoint.CurrentLevel currentLevel)
    {
        PlayerPrefs.SetString(currentLevel.ToString(), "unlocked");
        PlayerPrefs.Save();
    }

    public void GetSavedHighScore()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    public void GetSavedTotalPageCount()
    {
        totalPageCount = (short)PlayerPrefs.GetInt("totalPageCount", 0);
    }

    public void GetSavedTotalACount()
    {
        totalACount = (short)PlayerPrefs.GetInt("totalACount", 0);
    }

    void UpdateHighScoreText()
    {
        if(highScore > 0)
        {
            pauseHighScore.text = highScore.ToString();
            gameOverHighscore.text.ToString();
        }
    }

    public void UpdatePageCount(string levelOfPage)
    {
        totalPageCount += 1;
        //levelConScript.PageFoundLevel(levelOfPage);
    }




}
