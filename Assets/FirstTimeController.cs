using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;

public class FirstTimeController : MonoBehaviour {

    public CanvasGroup firstTimeCanvas;
    public CanvasGroup firstTimeGate15;
    public CanvasGroup firstTimeLocationPopup;
    public CanvasGroup shopPanelCanvas;
    public CanvasGroup pricePanelCanvas;
    public CanvasGroup valuePanelCanvas;

    public AudioSource audSource;
    protected Settings settingsScript;

	// Use this for initialization
	void Start () {
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        OpenFirstTimeCanvas();
    }


    public void OpenFirstTimeCanvas()
    {
        if(GetOpenedCanvas(firstTimeCanvas.ToString()) == "notOpened")
        {
			Handheld.PlayFullScreenMovie ("intro.mp4", Color.black, FullScreenMovieControlMode.Full); 
            OpenCanvas(firstTimeCanvas);
            SaveOpenedCanvas(firstTimeCanvas);
        }
    }

    public void OpenFirstTimeGate15()
    {
        if(GetOpenedCanvas(firstTimeGate15.ToString()) == "notOpened")
        {
            CloseCanvas(firstTimeCanvas);
            OpenCanvas(firstTimeGate15);
            SaveOpenedCanvas(firstTimeGate15);
        }
    }

    public void CloseFirstTimeGate15()
    {
        CloseCanvas(firstTimeGate15);
    }

    public void OpenShopPanelCanvas()
    {
        if (GetOpenedCanvas(shopPanelCanvas.ToString()) == "notOpened")
        {
            OpenCanvas(shopPanelCanvas);
            SaveOpenedCanvas(shopPanelCanvas);
        }
    }

    public void OpenPricePanelCanvas()
    {
        if(GetOpenedCanvas(pricePanelCanvas.ToString()) == "notOpened")
        {
            CloseCanvas(shopPanelCanvas);
            OpenCanvas(pricePanelCanvas);
            SaveOpenedCanvas(pricePanelCanvas);
        }
    }

    public void OpenValuePanelCanvas()
    {
        if (GetOpenedCanvas(valuePanelCanvas.ToString()) == "notOpened")
        {
            CloseCanvas(pricePanelCanvas);
            OpenCanvas(valuePanelCanvas);
            SaveOpenedCanvas(valuePanelCanvas);
        }
    }

    public void CloseValuePanelCanvas()
    {
        CloseCanvas(valuePanelCanvas);
    }

    //public void OpenFirstTimeLevel()
    //{
    //    if(GetOpenedCanvas(firstTimeLevel.ToString()) == "notOpened")
    //    {
    //        OpenCanvas(firstTimeLevel);
    //        SaveOpenedCanvas(firstTimeLevel);
    //    }
    //}

    //public void CloseFirstTimeLevel()
    //{
    //    CloseCanvas(firstTimeLevel);
    //}

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
