using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public GameObject gameOverObject;
    public GameObject winObject;
    WinGame winScript;
    public GameObject pauseObject;
    public Player playerScript;
    public GameMaster gmScript;

    void Start()
    {
        winScript = winObject.GetComponent<WinGame>();
    }

    public void WinPopup()
    {
        Time.timeScale = 0;
        gmScript.SaveScore();
        winScript.ChangePageSprite();
        winObject.SetActive(true);
    }

    public void GameOverPopUp()
    {
        Time.timeScale = 0;
        gmScript.SaveScore();
        if (gameOverObject) // clear error
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
