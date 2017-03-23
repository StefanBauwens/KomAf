using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {

    public GameObject gameOverObject;
    public GameObject winObject;
    public GameObject pauseObject;
    public Player playerScript;
    public GameMaster gmScript;

    public void WinPopup()
    {
        Time.timeScale = 0;
        gmScript.SaveScore();
        winObject.SetActive(true);
    }

    public void GameOverPopUp()
    {
        Time.timeScale = 0;
        gmScript.SaveScore();
        if(gameOverObject != null) // clear missing reference error
        {
            gameOverObject.SetActive(true);
        }
        
    }

    public void PausePopup()
    {
        playerScript.isPaused = true;
        Time.timeScale = 0;
        gmScript.SaveScore();
        pauseObject.SetActive(true);
    }

    public void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
        playerScript.isPaused = false;
    }



}
