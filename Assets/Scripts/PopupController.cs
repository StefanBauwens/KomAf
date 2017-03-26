using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public Canvas gameOverCanvas;
    public Canvas winCanvas;
    private GameMaster gmScript;
    public Canvas pauseCanvas;
    private CanvasGroup tempCanvasGroup;
    public Player playerScript;

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    public void WinPopup()
    {
        Time.timeScale = 0;
        gmScript.UpdateWinCoinText();
        //winScript.ChangePageSprite();
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

    void SetPopupVisible(Canvas popup, bool setVisible)
    {
        tempCanvasGroup = popup.GetComponent<CanvasGroup>();

        if (setVisible)
        { 
            tempCanvasGroup.alpha = 1;
            tempCanvasGroup.interactable = true;
            tempCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            tempCanvasGroup.alpha = 0;
            tempCanvasGroup.interactable = false;
            tempCanvasGroup.blocksRaycasts = false;
        }
    }


}
