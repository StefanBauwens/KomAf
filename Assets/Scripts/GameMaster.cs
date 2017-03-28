using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    private static GameMaster instanceRef;
    public int coinsCollectedInLevel;
    public short totalPageCount;
    public bool playingLevel;
    protected string tempLevel;
    private Text coinText;
    public Text winCoinText;
    WinGame winScript;
    Page pageScript;
    public LevelController levelConScript;
    

    void Awake()
    {
        if(instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playingLevel)
        {
            coinText.text = coinsCollectedInLevel.ToString();
        }
    }

    public void UpdateWinCoinText()
    {
        winCoinText.text = coinsCollectedInLevel.ToString();
    }

    public void SaveCoins(string level)
    {
        PlayerPrefs.SetInt("coinsCollectedInLevel"+ level, coinsCollectedInLevel);
    }

    public void SavePageCount()
    {
        PlayerPrefs.SetInt("totalPageCount", totalPageCount);
    }

    public void SaveUnlockedLevel(LevelPoint level)
    {
        for(int i = 0; i < levelConScript.levels.Length; i++)
        { 
            if (levelConScript.levels[i].ToString() == level.ToString())
            {
                PlayerPrefs.SetString("locked/unlocked" + levelConScript.levels[i].ToString(), "unlocked");
                PlayerPrefs.Save();
                Debug.Log("unlock levels opgeslagen: " + levelConScript.levels[i].ToString());
            }
        }
    }

    public int GetCoinsCollectedInLevel(string level)
    {
        return PlayerPrefs.GetInt("coinsCollectedInLevel" + level, 0);
    }

    public void SetCoinsCollectedInLevel()
    {
        coinsCollectedInLevel = GetCoinsCollectedInLevel(SceneManager.GetActiveScene().name);
    }


    public void UpdatePageCount(string levelOfPage)
    {
        totalPageCount += 1;
    }

    public void GetGameObjectsFromScene()
    {
        coinText = GameObject.Find("Canvas/CoinUI/CoinText").GetComponent<Text>();
        winCoinText = GameObject.Find("Canvas/WinCanvas/WinPopup/winCoinText").GetComponent<Text>();
        if (GameObject.Find("Page") != null)
        {
            pageScript = GameObject.Find("Page").GetComponent<Page>();
        }
        else
        {
            pageScript = null;
        }
        playingLevel = true;
    }


    public void SaveProgress(string currentLevel)
    {
        tempLevel = currentLevel;
        SaveCoins(tempLevel);
        SavePageCount();
        PlayerPrefs.Save();
    }




}
