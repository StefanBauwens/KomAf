using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    LevelController levelController;
    public GameObject locationPopup;
    LocationPopup locationPopupScript;
    GameMaster gmScript;
    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);   
    //}

    private void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        locationPopupScript = locationPopup.GetComponent<LocationPopup>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        gmScript.playingLevel = false;
        SceneManager.LoadScene("AntwerpMap");
        //levelController.CheckLevelUnlocked();
    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        gmScript.playingLevel = true;
    }

    public void OpenLocationPopup(string locationName)
    {
        locationPopup.SetActive(true);
        locationPopupScript.locationName = locationName;
    }
}
