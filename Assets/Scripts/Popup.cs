using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

    public GameObject gameOverObject;
    public GameObject winObject;

    private void Update()
    {
        
    }

    public void WinPopup()
    {
        Time.timeScale = 0;
        winObject.SetActive(true);
    }

    public void GameOverPopUp()
    {
        Time.timeScale = 0;
        gameOverObject.SetActive(true);
    }
    

    
}
