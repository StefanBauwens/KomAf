using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

    public GameObject popUp;
    public Text title;

    public Image extraImage;
    public Sprite dangerSprite;
    public Sprite pageFound;
    public Sprite pageNotFound;

    public Button returnButton;
    public Button menuButton;

    protected short pageCountToWin;
    public bool collectedPage = false;

    private void Update()
    {
        // TEST
        if(Input.GetKeyDown(KeyCode.G))
        {
            GameOverPopup();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            WinPopup();     
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            collectedPage = true;
        }
        
    }

    public void GameOverPopup()
    {
        popUp.SetActive(true);
        title.text = "U BEVINDT ZICH OP\nONBEKEND TERREIN!";
        extraImage.GetComponent<Image>().sprite = dangerSprite;
        returnButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void WinPopup()
    {
        popUp.SetActive(true);
        title.text = "LOCATIE IS\nHELEMAAL VERKEND!";
        if(collectedPage)
        {
            extraImage.GetComponent<Image>().sprite = pageFound;
        }
        else
        {
            extraImage.GetComponent<Image>().sprite = pageNotFound;
        }
        returnButton.gameObject.SetActive(true);
    }
}
