using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : PopupManager{

    protected short pageCountToWin;
    public Sprite pageFound;
    public Sprite pageNotFound;
    public Image pageImage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Check to see if total ammount of A's are collected
        
	}


    public void ChangePageSprite()
    {
            pageImage.GetComponent<Image>().sprite = pageFound;
    }

}
