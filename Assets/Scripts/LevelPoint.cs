using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPoint : StopPoint {

    public enum CurrentLevel
    {
		SintAnnastrand, Boekenbergpark, Vlaeykensgang, DeRuien, Kammenstraat, Erfgoedbibliotheek, Chinatown
    };
    public enum NextLevel
    {
		SintAnnastrand, Boekenbergpark, Vlaeykensgang, DeRuien, Kammenstraat, Erfgoedbibliotheek, Chinatown
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
        CheckMaxCoinsLevel();
        CheckMinValue();
        sceneConScript.OpenLocationPopup(gameObject.name, maxCoins, minValue);
    }

    protected void CheckMaxCoinsLevel()
    {
        switch(gameObject.name)
        {
            case "SintAnnastrand":
                maxCoins = locationInfoScript.maxCoinsSintannastrand;
                break;
            case "Boekenbergpark":
                maxCoins = locationInfoScript.maxCoinsBoekenbergpark;
                break;
            case "Vlaeykensgang":
                maxCoins = locationInfoScript.maxCoinsVlaeykensgang;
                break;
            case "DeRuien":
                maxCoins = locationInfoScript.maxCoinsDeruien;
                break;
            case "Kammenstraat":
                maxCoins = locationInfoScript.maxCoinsKammenstraat;
                break;
            case "Erfgoedbibliotheek":
                maxCoins = locationInfoScript.maxCoinsErfgoedbibliotheek;
                break;
            case "Chinatown":
                maxCoins = locationInfoScript.maxCoinsChinatown;
                break;
        }   
    }

    protected void CheckMinValue()
    {
        switch (gameObject.name)
        {
            case "SintAnnastrand":
                minValue = locationInfoScript.minValueSintannastrand;
                break;
            case "Boekenbergpark":
                minValue = locationInfoScript.minValueBoekenbergpark;
                break;
            case "Vlaeykensgang":
                minValue = locationInfoScript.minValueVlaeykensgang;
                break;
            case "DeRuien":
                minValue = locationInfoScript.minValueDeruien;
                break;
            case "Kammenstraat":
                minValue = locationInfoScript.minValueKammenstraat;
                break;
            case "Erfgoedbibliotheek":
                minValue = locationInfoScript.minValueErfgoedbibliotheek;
                break;
            case "Chinatown":
                minValue = locationInfoScript.minValueChinatown;
                break;
        }
    }













}
