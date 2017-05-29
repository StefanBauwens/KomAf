﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour {

    public Text totalCoinsText;
    public DisguiseItem[] itemArray;
	protected List<DisguiseItem> itemList;
    public List<string> itemsBought;
    public string currentItem;
	public int currentValue; //value of current item
    public int testCoins;
    private GameMaster gmScript;
    private int nrOfItems;
    public CanvasGroup shopCanvasGroup;
	protected bool hasRun;
    public GATE15 gate15Script;
    public Settings settingsScript;
    public FirstTimeController ftConScript;
    public Sprite buttonLongDisabled;
    public Sprite buttonLongEnabled;

    public AudioSource audSource;
    public AudioClip buySound;
    public AudioClip selectSound;

	protected Navigator playerMap;



    // Use this for initialization
    void Start () {
		playerMap = GameObject.FindGameObjectWithTag ("navigator").GetComponent<Navigator> ();
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        ftConScript = GameObject.FindGameObjectWithTag("FirstTimeController").GetComponent<FirstTimeController>();
        audSource = GetComponent<AudioSource>();
        nrOfItems = gmScript.nrOfItems;
        itemArray = new DisguiseItem[nrOfItems];
        itemsBought = new List<string>();
		itemList = new List<DisguiseItem> ();
		hasRun = false;
	}

	void Update()
	{
		if (!hasRun) {
			hasRun = true;
			SetupShop ();
		}
	}

	public void SetupShop()
    {
        for (int i = 0; i < nrOfItems; i++)
        {
            itemArray[i] = transform.GetChild(i).gameObject.GetComponent<DisguiseItem>();
        }

        // add bought items to list
        for (int itemNumber = 0; itemNumber < nrOfItems; itemNumber++)
        {
            if(gmScript.GetItemsBought(itemNumber) != "itemNotBought")
            {
                itemsBought.Add(gmScript.GetItemsBought(itemNumber));
            }
            else if(gmScript.GetItemsBought(itemNumber) == "itemNotBought")
            {
                itemsBought.Add("itemNotBought");
            }
        }

        // change boolean (from items in list) to bought
        for(int j = 0; j < nrOfItems; j++)
        {
            if( itemsBought.Contains(itemArray[j].ToString()))
            {
                itemArray[j].itemBought = true;
            }
        }

        currentItem = gmScript.GetCurrentDisguise();

        for (int j = 0; j < nrOfItems; j++)
        {
            itemArray[j].CheckItemState();
        }
    }

    public void RefreshShop()
    {
        for(int j = 0; j < itemArray.Length; j++)
        {
            itemArray[j].CheckItemState();
        }
        totalCoinsText.text = GameMaster.totalCoins.ToString();
    }

    public void ExitShop()
    {
		playerMap.popupsOpen = false;
        settingsScript.PlayButtonSound(audSource);
        gmScript.SaveCurrentDisguise(currentItem); 
		gmScript.SaveCurrentDisguiseValue (currentValue);
        for(int itemNr = 0; itemNr < nrOfItems; itemNr++)
        {
            if(itemArray[itemNr].itemBought)
            {
                gmScript.SaveItemsBought(itemArray[itemNr].ToString(), itemNr);
            }    
        }
        gmScript.SaveTotalCoins();

        shopCanvasGroup.alpha = 0;
        shopCanvasGroup.interactable = false;
        shopCanvasGroup.blocksRaycasts = false;
    }

    public void OpenShop()
    {
		playerMap.popupsOpen = true;
        ftConScript.OpenShopPanelCanvas();
        settingsScript.PlayButtonSound(audSource);
        gate15Script.ExitGate15();
		playerMap.popupsOpen = true;
        shopCanvasGroup.alpha = 1;
        shopCanvasGroup.interactable = true;
        shopCanvasGroup.blocksRaycasts = true; 
    }

}
