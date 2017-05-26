using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimeLevel : MonoBehaviour {

    protected CanvasGroup firstTimeLevel;

    public AudioSource audSource;
    protected Settings settingsScript;

    // Use this for initialization
    void Start () {
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        firstTimeLevel = this.GetComponent<CanvasGroup>();
    }

    public void OpenFirstTimeLevel()
    {
        if (GetOpenedCanvas(firstTimeLevel.ToString()) == "notOpened")
        {
            OpenCanvas(firstTimeLevel);
            SaveOpenedCanvas(firstTimeLevel);
        }
    }

    public void CloseFirstTimeLevel()
    {
        CloseCanvas(firstTimeLevel);
    }

    void SaveOpenedCanvas(CanvasGroup canvasName)
    {
        Debug.Log("save canvas: " + canvasName);
        PlayerPrefs.SetString(canvasName.ToString(), "opened");
    }

    string GetOpenedCanvas(string canvasName)
    {
        return PlayerPrefs.GetString(canvasName, "notOpened");
    }

    void OpenCanvas(CanvasGroup openCanvas)
    {
        settingsScript.PlayButtonSound(audSource);
        openCanvas.alpha = 1;
        openCanvas.interactable = true;
        openCanvas.blocksRaycasts = true;
    }

    void CloseCanvas(CanvasGroup closeCanvas)
    {
        settingsScript.PlayButtonSound(audSource);
        closeCanvas.alpha = 0;
        closeCanvas.interactable = false;
        closeCanvas.blocksRaycasts = false;
    }
}
