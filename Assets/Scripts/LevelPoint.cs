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
        sceneConScript.OpenLocationPopup(gameObject.name, maxCoins);
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













}
