using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseItem : MonoBehaviour {

    public Image disguiseImage;
    public Image selectImage;
    public Text disguiseName;
    public Text disguiseDescription;
    public Text disguisePrice;
    public GameObject buyButtonObject;
    public GameObject selectButtonObject;
    private Button buyButton;
    private Button selectButton;
    public bool itemBought = false;
    private bool itemSelected = false;
    private ShopList shopScript;
    

    public Sprite itemSprite;
    public Sprite selectSprite;
    public Sprite deselectSprite;
    public string itemName;
    public string itemDescription;
    public int itemPrice;

	// Use this for initialization
	void Start () {
        shopScript = GetComponentInParent<ShopList>();
        buyButton = buyButtonObject.GetComponent<Button>();
        selectButton = selectButtonObject.GetComponent<Button>();
        ItemSetup();
    }

    public void ItemSetup()
    {
        disguiseImage.sprite = itemSprite;
        disguiseName.text = itemName;
        disguiseDescription.text = itemDescription;
        disguisePrice.text = itemPrice.ToString();
        shopScript.SetupShop();
        //CheckItemState();
    }

    public void CheckItemState()
    {
        CheckItemBought();
        CheckItemSelected();
    }

    public void CheckItemBought()
    {
        if(itemBought)
        {
            buyButton.GetComponentInChildren<Text>().text = "Gekocht";
        }
        else
        {
            buyButton.GetComponentInChildren<Text>().text = "Kopen";
        }
        
        CheckBuyButtonInteractable();
    }

    public void CheckItemSelected()
    {
        if(itemBought && this.ToString() != shopScript.currentItem)
        {
            selectButton.GetComponentInChildren<Text>().text = "Selecteren";
            selectImage.sprite = deselectSprite;
            selectButton.interactable = true;
            itemSelected = false;
        }
        else if (this.ToString() == shopScript.currentItem)
        {
            selectButton.GetComponentInChildren<Text>().text = "Geselecteerd";
            selectImage.sprite = selectSprite;
            selectButton.interactable = false;
            itemSelected = true;
            Debug.Log("current item in checkitemselected: " + shopScript.currentItem);
        }
        else
        {
            selectButton.GetComponentInChildren<Text>().text = "Niet in bezit";
            selectImage.sprite = deselectSprite;
            selectButton.interactable = false;
        }
    }

    public void CheckBuyButtonInteractable()
    {
        //if(GameMaster.totalCoins > itemPrice)

        if (shopScript.testCoins >= itemPrice && !itemBought)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }


    public void BuyItem()
    {
        //if(!itemBought && GameMaster.totalCoins > itemPrice)
        if(!itemBought && shopScript.testCoins >= itemPrice)
        {
            //GameMaster.totalCoins -= itemPrice;
            shopScript.testCoins -= itemPrice;
            itemBought = true;
            shopScript.itemsBought.Add(this.ToString());
            buyButton.interactable = false;
            shopScript.RefreshShop();
        }
    }

    public void SelectItem()
    {
        if(itemBought && !itemSelected)
        {
            selectImage.sprite = selectSprite;
            itemSelected = true;
            shopScript.currentItem = this.ToString();
            Debug.Log("current item: " + shopScript.currentItem);
            shopScript.RefreshShop();
        }
    }
}
