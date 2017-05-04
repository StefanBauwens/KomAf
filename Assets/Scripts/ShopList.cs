using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour {

    public DisguiseItem[] itemArray;
    public List<string> itemsBought;
    public string currentItem;
    public Text totalCoinsShop;
    public int testCoins;
    private GameMaster gmScript;
    private int nrOfItems;
    public CanvasGroup shopCanvasGroup;


    // Use this for initialization
    void Start () {
        
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
        nrOfItems = gmScript.nrOfItems;
        itemArray = new DisguiseItem[nrOfItems];
        List<string> itemsBought = new List<string>();
        
        //SetupShop();
	}

    public void SetupShop()
    {
        //totalCoinsShop.text = GameMaster.totalCoins.ToString();
        totalCoinsShop.text = testCoins.ToString();

        

        for (int i = 0; i < nrOfItems; i++)
        {
            itemArray[i] = transform.GetChild(i).gameObject.GetComponent<DisguiseItem>();
            Debug.Log(itemArray[i]);
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
            Debug.Log("itemname: " + itemArray[j] + "name in list: " + itemsBought[j]);
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

        totalCoinsShop.text = testCoins.ToString();
    }

    public void ExitShop()
    {
        gmScript.SaveCurrentDisguise(currentItem);
        for(int itemNr = 0; itemNr < nrOfItems; itemNr++)
        {
            if(itemArray[itemNr].itemBought)
            {
                gmScript.SaveItemsBought(itemArray[itemNr].ToString(), itemNr);
                Debug.Log("saved items bought: " + itemArray[itemNr].ToString());
            }    
        }

        shopCanvasGroup.alpha = 0;
        shopCanvasGroup.interactable = false;
        shopCanvasGroup.blocksRaycasts = false;
    }

}
