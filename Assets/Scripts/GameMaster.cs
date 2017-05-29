using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    private static GameMaster instanceRef;
    public int pageCount;
    public bool pageCollected;
    public bool playingLevel;
    protected string tempLevel;
    private Text coinText;
    public Text winCoinText;
    WinGame winScript;
    Page pageScript;
    public LevelController levelConScript;
    public static int totalCoins;
    public Text totalCoinsText;
    public List<Vector3> collectedCoinsPos;
    public int maxCoins;
    public Text maxCoinsText;
    public int coinsCollectedInLevel;
    public int nrOfItems = 10;
	public int currentValue;

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
		currentValue = PlayerPrefs.GetInt ("currentDisguiseValue", 0);
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

    public void UpdatePauseMenuCoinText(Text pauseMenu)
    {
        pauseMenu.text = coinsCollectedInLevel.ToString();
    }

    public void SaveCoins(string level)
    {
        PlayerPrefs.SetInt("coinsCollectedInLevel"+ level, coinsCollectedInLevel);
        //Debug.Log(coinsCollectedInLevel + " coins saved in level: " + level);
    }

    public void UpdateTotalCoins(string level)
    {
        //Debug.Log("coinscollectedinlevel: " + coinsCollectedInLevel + " coins from save file: " + GetCoinsCollectedInLevel(level));
        if (coinsCollectedInLevel > GetCoinsCollectedInLevel(level))
        {
            totalCoins -= GetCoinsCollectedInLevel(level);
            totalCoins += coinsCollectedInLevel;
            SaveTotalCoins();
        }
    }

    public void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }

    public void SetMaxCoins()
    {
        maxCoinsText = GameObject.FindGameObjectWithTag("MaxCoins").GetComponent<Text>();
        maxCoinsText.text = maxCoins.ToString();
        maxCoins = 0;
    }

    public void SaveUnlockedLevel(LevelPoint level)
    {
        for(int i = 0; i < levelConScript.levels.Length; i++)
        { 
            if (levelConScript.levels[i].ToString() == level.ToString())
            {
                PlayerPrefs.SetString("locked/unlocked" + levelConScript.levels[i].ToString(), "unlocked");
                PlayerPrefs.Save();
            }
        }
    }

    public void AddCollectedCoinPosition(Vector3 coinPos)
    {
        collectedCoinsPos.Add(coinPos);
    }

    public void SaveCollectedCoinPositions(string level)
    {
        for(int i = 0; i < collectedCoinsPos.Count; i++)
        {
            PlayerPrefs.SetString("coinPosition"+ i + level, collectedCoinsPos[i].x.ToString() + "," + collectedCoinsPos[i].y.ToString());
        }
        PlayerPrefs.Save();
        collectedCoinsPos.Clear();
    }

    public void GetCollectedCoinPositions(string level)
    {
        for(int i = 0; i < GetCoinsCollectedInLevel(level.ToString()); i++)
        {
            if(PlayerPrefs.GetString("coinPosition" + i + level, "noPositionFound") != "noPositionFound")
            {
                AddCollectedCoinPosition(StringToVector3(PlayerPrefs.GetString("coinPosition" + i + level, "noPositionFound")));
            }
        }
    }

    public int GetCoinsCollectedInLevel(string level)
    {
        return PlayerPrefs.GetInt("coinsCollectedInLevel" + level, 0);
    }

    public int GetTotalCoins()
    {
        return PlayerPrefs.GetInt("totalCoins", 0);
    }

    public void SetCoinsCollectedInLevel()
    {
        coinsCollectedInLevel = GetCoinsCollectedInLevel(SceneManager.GetActiveScene().name);
    }

    public void SavePageCount()
    {
        PlayerPrefs.SetInt("PageCount", pageCount);
        PlayerPrefs.Save();
    }

    public void GetPageCount()
    {
        pageCount = PlayerPrefs.GetInt("PageCount", 0);
    }

    public void SavePageCollected(string level)
    {
        if (pageCollected)
        {
            PlayerPrefs.SetString("pageCollected" + level, "true");
            pageCount++;
            SavePageCount();
            pageCollected = false;
        }
    }

    public bool CheckPageCollected(string level)
    {
        if(PlayerPrefs.GetString("pageCollected" + level, "false") == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetGameObjectsFromScene()
    {
        coinText = GameObject.Find("Canvas/CoinUI/CoinText").GetComponent<Text>();
        winCoinText = GameObject.Find("WinCanvas/WinPopup/winCoinText").GetComponent<Text>();
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
        UpdateTotalCoins(tempLevel);
        SaveCollectedCoinPositions(tempLevel);
        SaveCoins(tempLevel);
        SavePageCollected(tempLevel);
        PlayerPrefs.Save();
    }

    public void UpdateTotalCoinUI()
    {
        totalCoins = GetTotalCoins();
        totalCoinsText = GameObject.Find("Canvas/CoinUI/totalCoins").GetComponent<Text>();
        totalCoinsText.text = totalCoins.ToString();
    }

    public void SaveCurrentDisguise(string currentDisguise)
    {
        PlayerPrefs.SetString("currentDisguise", currentDisguise);
        PlayerPrefs.Save();
    }

	public void SaveCurrentDisguiseValue(int currentDisguiseValue)
	{
		currentValue = currentDisguiseValue;
		PlayerPrefs.SetInt("currentDisguiseValue", currentDisguiseValue);
		PlayerPrefs.Save();
	}

    public string GetCurrentDisguise()
    {
        return PlayerPrefs.GetString("currentDisguise", "noDisguiseSelected");
    }

    public void SaveItemsBought(string itemName, int itemNumber)
    {
            PlayerPrefs.SetString("itemsBought" + itemNumber, itemName);
            PlayerPrefs.Save();
    }

    public string GetItemsBought(int itemNumber)
    {
        return PlayerPrefs.GetString("itemsBought" + itemNumber, "itemNotBought");
    }

    public static Vector3 StringToVector3(string sVector)
    {
        string[] sArray = sVector.Split(',');
        Vector3 result = new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]), 0);
        return result;
    }

}
