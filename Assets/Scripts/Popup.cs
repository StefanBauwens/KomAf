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
        winObject.SetActive(false);
        gameOverObject.SetActive(true);
    }

    public void GameOverPopUp()
    {
        gameOverObject.SetActive(false);
        winObject.gameObject.SetActive(true);
    }
    

    
}
