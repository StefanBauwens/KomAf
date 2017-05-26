using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    protected AudioSource audSource;
    public AudioClip buttonSound;
    private static SceneController instanceRef;
    public LevelController levelConScript;
    public GameMaster gmScript;
    LocationPopup locationPopupScript;
    EndOfLevel endOfLevel;
    protected string tempLevel;
    CanvasGroup locationPopupCanvas;
    LevelKeeper levelKeeper;
    protected bool tempLevelFinished;
    public string AntwerpMap;
    protected PopupController popupScript;
    protected Settings settingsScript;
    protected FirstTimeLevel ftLevelScript;

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
        
        audSource = GetComponent<AudioSource>();
        
    }

    public void RestartLevel()
    {
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
        gmScript.collectedCoinsPos.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
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
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
        SetLocationPopupCanvasVisible(false);
        SceneManager.LoadScene(sceneName);

        //if (sceneName == "SintAnnastrand")
        //{
        //    ftLevelScript = GameObject.FindGameObjectWithTag("FirstTimeLevel").GetComponent<FirstTimeLevel>();
        //    if (ftLevelScript)
        //    {
        //        Debug.Log("beneden");
        //    }
        //    ftLevelScript.OpenFirstTimeLevel();
        //}
    }

    public void OpenLocationPopup(string locationName, int maxCoins, int minValue)
    {
        audSource.PlayOneShot(buttonSound, settingsScript.volumeSE);
        locationPopupScript.locationName = locationName;
        locationPopupScript.CheckLocationInfoText();
        locationPopupScript.coinsCollectedText.text = gmScript.GetCoinsCollectedInLevel(locationName).ToString();
        locationPopupScript.maxCoins.text = maxCoins.ToString();
        locationPopupScript.minValue.text = minValue.ToString();
        locationPopupScript.locationNameText.text = locationPopupScript.ChangeLocationName(locationName);
        SetLocationPopupCanvasVisible(true);  
    }

    public void SendCurrentLevel(string currentLevel, bool levelFinished)
    {
        tempLevel = currentLevel;
        tempLevelFinished = levelFinished;
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += LevelFinishedLoading; // start listening for scene change when script enabled
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
            settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
            popupScript = GameObject.FindGameObjectWithTag("PopupController").GetComponent<PopupController>();
            if (activeScene.name == "SintAnnastrand")
            {
                ftLevelScript = GameObject.FindGameObjectWithTag("FirstTimeLevel").GetComponent<FirstTimeLevel>();
                if (ftLevelScript)
                {
                    Debug.Log("found script");
                }
                ftLevelScript.OpenFirstTimeLevel();
            }

            gmScript.GetGameObjectsFromScene();
            gmScript.SetCoinsCollectedInLevel();

        }
        else if (activeScene.name == AntwerpMap)
        {
            settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
            settingsScript.settingsButtonCanvas = GameObject.FindGameObjectWithTag("SettingsButton").GetComponent<Button>();
            settingsScript.settingsButtonCanvas.onClick.AddListener(() => settingsScript.OpenSettings());
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
