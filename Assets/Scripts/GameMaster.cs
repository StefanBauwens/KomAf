using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    protected int highScore;
    public int score;
    public Text scoreText;
    

	// Use this for initialization
	void Start ()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
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
        }
    }
}
