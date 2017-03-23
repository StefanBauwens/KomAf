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

    public Text scoreText;
    public Text pauseHighScore;
    public Text gameOverHighscore;
    public Text winScore;
    public WinGame winScript;
    public Page pageScript;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        UpdateHighScoreText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = score.ToString();
	}

    public void SaveScore()
    {
        winScore.text = score.ToString();
        
        totalACount += winScript.CountAPoints();
        pageScript.UpdatePageCount();

        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            highScore = score;
            UpdateHighScoreText();
        }
    }

    //public void SaveUnlockedLevel()
    //{

    //}

    void UpdateHighScoreText()
    {
        if(highScore > 0)
        {
            pauseHighScore.text = highScore.ToString();
            gameOverHighscore.text.ToString();
        }
    }

    
    
    
}
