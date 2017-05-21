using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPoint : StopPoint {

    public enum CurrentLevel
    {
		SintAnnastrand, Erfgoedbibliotheek, Chinatown, Kammenstraat, Vlaeykensgang, DeRuien, Boekenbergpark, ufoplace
    };
    public enum NextLevel
    {
        SintAnnastrand, Erfgoedbibliotheek, Chinatown, Kammenstraat, Vlaeykensgang, DeRuien, Boekenbergpark, ufoplace
    };
    public CurrentLevel currentLevel;
    public NextLevel nextLevel;
    public bool levelUnlocked;
    public int maxCoins;
    protected int minValue;
    private Button levelButton;
    private SceneController sceneConScript;
    protected LocationInfoKeeper locationInfoScript;
    

	// Use this for initialization
	void Start () {
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        locationInfoScript = GameObject.FindGameObjectWithTag("LocationInfoKeeper").GetComponent<LocationInfoKeeper>();
    }

    //call this from stefan's script when navigator has reached this level instead of using a button
    public void HasClickedOnLevel()
    {
		if (this.currentLevel != CurrentLevel.ufoplace) {
			CheckLevelInfo ();
			sceneConScript.OpenLocationPopup (gameObject.name, maxCoins, minValue);
		} else {
			//DISPLAY WIN POPUP You have found your ufo bla bla
		}
        
    }

    protected void CheckLevelInfo()
    {
        switch(gameObject.name)
        {
            case "SintAnnastrand":
                maxCoins = locationInfoScript.maxCoinsSintannastrand;
                minValue = locationInfoScript.minValueSintannastrand;
                break;
            case "Erfgoedbibliotheek":
                maxCoins = locationInfoScript.maxCoinsErfgoedbibliotheek;
                minValue = locationInfoScript.minValueErfgoedbibliotheek;
                break;
            case "Chinatown":
                maxCoins = locationInfoScript.maxCoinsChinatown;
                minValue = locationInfoScript.minValueChinatown;
                break;
            case "Kammenstraat":
                maxCoins = locationInfoScript.maxCoinsKammenstraat;
                minValue = locationInfoScript.minValueKammenstraat;
                break;
            case "Vlaeykensgang":
                maxCoins = locationInfoScript.maxCoinsVlaeykensgang;
                minValue = locationInfoScript.minValueVlaeykensgang;
                break;
            case "DeRuien":
                maxCoins = locationInfoScript.maxCoinsDeruien;
                minValue = locationInfoScript.minValueDeruien;
                break; 
            case "Boekenbergpark":
                maxCoins = locationInfoScript.maxCoinsBoekenbergpark;
                minValue = locationInfoScript.minValueBoekenbergpark;
                break;
        }   
    }
}
