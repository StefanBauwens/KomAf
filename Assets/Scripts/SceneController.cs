using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private static SceneController instanceRef;
    public LevelController levelConScript;
    public GameMaster gmScript;
    LocationPopup locationPopupScript;
    EndOfLevel endOfLevel;
    protected EndOfLevel.CurrentLevel tempLevel;
    CanvasGroup locationPopupCanvas;
    LevelKeeper levelKeeper;
    private bool tempLevelFinished;
	public string AntwerpMap = "AntwerpMap";

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
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        gmScript.playingLevel = false;
        if (tempLevelFinished)
        {
            gmScript.SaveCollectedCoinPositions(tempLevel); // it can only save when level completed --> test with pause menu, go to antwerp map and back (still have to connect buttons with functions)
        }
        SceneManager.LoadScene(AntwerpMap); 
    }


    public void LoadLevelByName(string sceneName)
    {
        SetLocationPopupCanvasVisible(false);
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLocationPopup(string locationName, int maxCoins)
    {
        locationPopupScript.locationName = locationName;
        locationPopupScript.locationText.text = locationName;
        locationPopupScript.coinsCollectedText.text = gmScript.GetCoinsCollectedInLevel(locationName).ToString();
        locationPopupScript.maxCoins.text = maxCoins.ToString();
        SetLocationPopupCanvasVisible(true);  
    }

    public void SendCurrentLevel(EndOfLevel.CurrentLevel currentLevel, bool levelFinished)
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
            endOfLevel = GameObject.Find("EndOfLevel").GetComponent<EndOfLevel>();
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
