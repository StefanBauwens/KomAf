using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPoint : StopPoint {

    public enum CurrentLevel
    {
		SintAnnastrand, ZwemvijverBoekenberg, Vlaeykensgang, DeRuien, Kammenstraat, ErfgoedBibliotheek, ChinaTown
    };
    public enum NextLevel
    {
		SintAnnastrand, ZwemvijverBoekenberg, Vlaeykensgang, DeRuien, Kammenstraat, ErfgoedBibliotheek, ChinaTown
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
            case "ZwemvijverBoekenberg":
                maxCoins = locationInfoScript.maxCoinsZwemvijverboekenberg;
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
            case "ErfgoedBibliotheek":
                maxCoins = locationInfoScript.maxCoinsErfgoedbibliotheek;
                break;
            case "ChinaTown":
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
            case "ZwemvijverBoekenberg":
                minValue = locationInfoScript.minValueZwemvijverboekenberg;
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
            case "ErfgoedBibliotheek":
                minValue = locationInfoScript.minValueErfgoedbibliotheek;
                break;
            case "ChinaTown":
                minValue = locationInfoScript.minValueChinatown;
                break;
        }
    }













}
