using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimeLevel : MonoBehaviour {

    public CanvasGroup firstTimeLevel;
    public PopupController popupConScript;
    protected Settings settingsScript;

    // Use this for initialization
    void Start () {
        OpenFirstTimeLevel();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
    }

    public void OpenFirstTimeLevel()
    {
        if (GetOpenedCanvas(firstTimeLevel.ToString()) == "notOpened")
        {
            popupConScript.OpenFirstTimeLevel(firstTimeLevel);
            SaveOpenedCanvas(firstTimeLevel);
        }
    }

    public void CloseFirstTimeLevel()
    {
        popupConScript.CloseFirstTimeLevel(firstTimeLevel);
    }

    void SaveOpenedCanvas(CanvasGroup canvasName)
    {
        PlayerPrefs.SetString(canvasName.ToString(), "opened");
    }

    string GetOpenedCanvas(string canvasName)
    {
        return PlayerPrefs.GetString(canvasName, "notOpened");
    }
}
