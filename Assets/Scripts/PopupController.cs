using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour {

    public Player playerScript;
    public CanvasGroup gameOverCanvas;
	public CanvasGroup gameOverCanvasDeath;
    public CanvasGroup winCanvas;
    public CanvasGroup pauseCanvas;
    private GameMaster gmScript;
    private AudioSource audSource;
    public AudioClip[] sounds;
    private AudioClip buttonSound;
    private AudioClip gameOverSound;
    private AudioClip winSound;
    protected Settings settingsScript;

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        audSource= gameObject.GetComponent<AudioSource>();
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == "button")
            {
                buttonSound = sounds[i];
            }
            else if(sounds[i].name == "gameOver")
            {
                gameOverSound = sounds[i];
            }
            else if(sounds[i].name == "win")
            {
                winSound = sounds[i];
            }
        }
    }

    public void WinPopup()
    {
        if (audSource)
        {
            audSource.PlayOneShot(winSound, settingsScript.volumeSE);
        }
        Time.timeScale = 0;
        gmScript.UpdateWinCoinText();
        SetPopupVisible(winCanvas, true);
    }

    public void GameOverPopUp()
    {
        if (audSource)
        {
            audSource.PlayOneShot(gameOverSound, settingsScript.volumeSE);
        }
        Time.timeScale = 0;
        if (gameOverCanvas)
        {
            SetPopupVisible(gameOverCanvas, true);
        }
        
    }

	public void GameOverPopUpDeath() //shows the popup for when the player touches an enemy
	{
		if (audSource)
		{
			audSource.PlayOneShot(gameOverSound, settingsScript.volumeSE);
		}
		Time.timeScale = 0;
		if (gameOverCanvasDeath)
		{
			SetPopupVisible(gameOverCanvasDeath, true);
		}

	}

    public void PausePopup()
    {
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
        playerScript.isPaused = true;
        Time.timeScale = 0;
        SetPopupVisible(pauseCanvas, true);
    }

    public void Resume()
    {
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
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
