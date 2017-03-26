using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private static SceneController instanceRef;
    LevelController levelConScript;
    LocationPopup locationPopupScript;
    GameMaster gmScript;
    EndOfLevel endOfLevel;
    protected EndOfLevel.CurrentLevel tempLevel;
    CanvasGroup locationPopupCanvas;
    LevelKeeper levelKeeper;

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

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        levelConScript = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        gmScript.playingLevel = false;
        SceneManager.LoadScene("AntwerpMap"); 
    }


    public void LoadLevelByName(string sceneName)
    {
        SetLocationPopupCanvasVisible(false);
        SceneManager.LoadScene(sceneName);
        gmScript.playingLevel = true;
    }

    public void OpenLocationPopup(string locationName)
    {
        Debug.Log("open location popup");

        locationPopupScript.locationName = locationName;
        locationPopupScript.locationText.text = locationName;
        locationPopupScript.coinsCollectedText.text = gmScript.GetCoinsCollectedInLevel(locationName).ToString();
        SetLocationPopupCanvasVisible(true);      
    }

    public void SendCurrentLevel(EndOfLevel.CurrentLevel currentLevel)
    {
        tempLevel = currentLevel;
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
        if (activeScene.name != "AntwerpMap")
        {
            endOfLevel = GameObject.Find("EndOfLevel").GetComponent<EndOfLevel>();
            gmScript.GetGameObjectsFromScene();
            gmScript.SetCoinsCollectedInLevel();
        }
        else if (activeScene.name == "AntwerpMap")
        { 
            locationPopupCanvas = GameObject.FindGameObjectWithTag("LocationPopupCanvas").GetComponent<CanvasGroup>();
            locationPopupScript = GameObject.FindGameObjectWithTag("LocationPopup").GetComponent<LocationPopup>();
            //DEBUG after second level, no level detected!
            levelConScript.CheckLevelUnlocked();
            levelConScript.SetLevelsFromArray();
            levelConScript.GetLevelUnlocker(tempLevel); // all level related code only in AntwerpMap!
         
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
