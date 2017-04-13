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
    public int coinsCollectedInLevel;

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

    public void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("totalCoins", totalCoins);
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
        Debug.Log("Saved page count: " + pageCount);
    }

    public void GetPageCount()
    {
        pageCount = PlayerPrefs.GetInt("PageCount", 0);
        Debug.Log("get page count from save file: " + pageCount);
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
        SaveTotalCoins();
        SavePageCount();
        PlayerPrefs.Save();
    }

    public void UpdateTotalCoinUI()
    {
        totalCoins = GetTotalCoins();
        totalCoinsText = GameObject.Find("Canvas/CoinUI/totalCoins").GetComponent<Text>();
        totalCoinsText.text = totalCoins.ToString();
    }

    public static Vector3 StringToVector3(string sVector)
    {
        string[] sArray = sVector.Split(',');
        Vector3 result = new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]), 0);
        return result;
    }

}
