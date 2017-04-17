using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private AudioSource buttonSound;
    private static SceneController instanceRef;
    public LevelController levelConScript;
    public GameMaster gmScript;
    LocationPopup locationPopupScript;
    EndOfLevel endOfLevel;
    protected string tempLevel;
    CanvasGroup locationPopupCanvas;
    LevelKeeper levelKeeper;
    private bool tempLevelFinished;
    public string AntwerpMap;
    protected PopupController popupScript;

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
        
        buttonSound = GetComponent<AudioSource>();
    }

    public void RestartLevel()
    {
        buttonSound.Play();
        gmScript.collectedCoinsPos.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        buttonSound.Play();
        gmScript.playingLevel = false;
        if (tempLevelFinished)
        {
            gmScript.SaveProgress(tempLevel);
            
            // code lines that run before AntwerpMap is loaded completely
            gmScript.GetPageCount();
        }
		SceneManager.LoadScene(AntwerpMap); 
    }


    public void LoadLevelByName(string sceneName)
    {
        buttonSound.Play();
        SetLocationPopupCanvasVisible(false);
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLocationPopup(string locationName, int maxCoins)
    {
        buttonSound.Play();
        locationPopupScript.locationName = locationName;
        locationPopupScript.locationNameText.text = locationName;
        locationPopupScript.CheckLocationInfo();
        locationPopupScript.coinsCollectedText.text = gmScript.GetCoinsCollectedInLevel(locationName).ToString();
        locationPopupScript.maxCoins.text = maxCoins.ToString();
        SetLocationPopupCanvasVisible(true);  
    }

    public void SendCurrentLevel(string currentLevel, bool levelFinished)
    {
        tempLevel = currentLevel;
        tempLevelFinished = levelFinished;
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += LevelFinishedLoading; // satrt listening for scene change when script enabled
    }

    void OnDisable()
    {
        // Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.activeSceneChanged -= LevelFinishedLoading; // stop listening for scene change when script disabled
    }

    void LevelFinishedLoading(Scene previousScene, Scene activeScene)
    {
        if (activeScene.name != AntwerpMap)
        {
            popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
            gmScript.GetGameObjectsFromScene();
            gmScript.SetCoinsCollectedInLevel();
        }
        else if (activeScene.name == AntwerpMap)
        {
            locationPopupCanvas = GameObject.FindGameObjectWithTag("LocationPopupCanvas").GetComponent<CanvasGroup>();
            locationPopupScript = GameObject.FindGameObjectWithTag("LocationPopup").GetComponent<LocationPopup>();

            levelConScript.SetLevelsFromArray();
            levelConScript.CheckLevelUnlocked();
            if (tempLevelFinished)
            {
                levelConScript.GetLevelUnlocker(tempLevel); // all level related code only in AntwerpMap!  
                tempLevelFinished = false;
            }

            gmScript.UpdateTotalCoinUI();
        }
    }


    void SetLocationPopupCanvasVisible(bool setVisible)
    {
        if(setVisible)
        {
            locationPopupCanvas.alpha = 1;
            locationPopupCanvas.interactable = true;
            locationPopupCanvas.blocksRaycasts = true;
        }
        else
        {
            locationPopupCanvas.alpha = 0;
            locationPopupCanvas.interactable = false;
            locationPopupCanvas.blocksRaycasts = false;
        }
    }
}
