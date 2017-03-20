using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    protected int highScore;
    public int score;
    public Text scoreText;
    public Text pauseHighScore;
    public Text gameOverHighscore;
    public Text winHighScore;
    

	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
        highScore = PlayerPrefs.GetInt("highScore", 0);
        pauseHighScore.text = highScore.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = score.ToString();
	}

    public void SaveScore()
    {
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
            highScore = score;
            pauseHighScore.text = highScore.ToString();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
