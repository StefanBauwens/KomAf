using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour {

    public DisguiseItem[] itemArray;
    public List<DisguiseItem> itemsBought;
    public string currentItem;
    public Text totalCoinsShop;
    public int testCoins;
    private int itemCount;
    private GameMaster gmScript;

    // Use this for initialization
    void Start () {
        
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
        SetupShop();
	}

    private void SetupShop()
    {
        //totalCoinsShop.text = GameMaster.totalCoins.ToString();
        totalCoinsShop.text = testCoins.ToString();


        currentItem = gmScript.GetCurrentDisguise();
        // ADD BOUGHT ITEMS TO LIST
        Debug.Log(gmScript.GetCurrentDisguise());

        itemArray = new DisguiseItem[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            itemArray[i] = transform.GetChild(i).gameObject.GetComponent<DisguiseItem>();
            Debug.Log(itemArray[i]);
        }

        for (int j = 0; j < itemArray.Length; j++)
        {
            itemArray[j].ItemSetup();
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
        for(int i = 0; i < itemsBought.Count; i++)
        {
            gmScript.SaveItemsBought(itemsBought[i].ToString());
            Debug.Log("saved items bought: " + itemsBought[i].ToString());
        }
        
        gameObject.SetActive(false);
    }

}
