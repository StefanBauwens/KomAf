using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public Player playerScript;
    public CanvasGroup gameOverCanvas;
    public CanvasGroup winCanvas;
    public CanvasGroup pauseCanvas;
    private GameMaster gmScript;
    

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void WinPopup()
    {
        Time.timeScale = 0;
        gmScript.UpdateWinCoinText();
        SetPopupVisible(winCanvas, true);
    }

    public void GameOverPopUp()
    {
        Time.timeScale = 0;
        if (gameOverCanvas) // clear error
        {
            SetPopupVisible(gameOverCanvas, true);
        }
    }

    public void PausePopup()
    {
        playerScript.isPaused = true;
        Time.timeScale = 0;
        SetPopupVisible(pauseCanvas, true);
    }

    public void Resume()
    {
        SetPopupVisible(pauseCanvas, false);
        Time.timeScale = 1;
        playerScript.isPaused = false;
    }

    void SetPopupVisible(CanvasGroup popup, bool setVisible)
    {
        if (setVisible)
        { 
            popup.alpha = 1;
            popup.interactable = true;
            popup.blocksRaycasts = true;
        }
        else
        {
            popup.alpha = 0;
            popup.interactable = false;
            popup.blocksRaycasts = false;
        }
    }


}
