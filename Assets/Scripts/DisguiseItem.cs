using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisguiseItem : MonoBehaviour {

    public Image disguiseImage;
    public Image selectImage;
    public Image valiueImage;
    public Text disguiseName;
    public Text disguiseDescription;
    public Text disguisePrice;
    public Text disguiseValue;
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
    public int itemValue;

    protected Color appelBlauw;

	//protected bool hasRun;

	// Use this for initialization
	void Start () {
        ColorUtility.TryParseHtmlString("#148a93", out appelBlauw);
        shopScript = GetComponentInParent<ShopList>();
        buyButton = buyButtonObject.GetComponent<Button>();
        selectButton = selectButtonObject.GetComponent<Button>();
		//hasRun = false;
        ItemSetup();
    }

	/*void Update()
	{
		if (!hasRun) {
			hasRun = true;
			ItemSetup ();
		}
	}*/

    public void ItemSetup()
    {
        disguiseImage.sprite = itemSprite;
        disguiseName.text = itemName;
        disguiseDescription.text = itemDescription;
        disguisePrice.text = itemPrice.ToString();
        disguiseValue.text = itemValue.ToString();
        //shopScript.SetupShop(this);
        //CheckItemState();
    }

    public void CheckItemState()
    {
        CheckItemBought();
        CheckItemSelected();
    }

    public void CheckItemBought()
    {
        Text bButtonText = buyButton.GetComponentInChildren<Text>();
        if (itemBought)
        {
            
            bButtonText.text = "Gekocht";
            bButtonText.color = Color.white;
            buyButton.image.sprite = shopScript.buttonLongDisabled;
        }
        else
        {
            bButtonText.text = "Kopen";
            //bButtonText.color = appelBlauw;
            bButtonText.color = appelBlauw;
            buyButton.image.sprite = shopScript.buttonLongEnabled;
        }
        
        CheckBuyButtonInteractable();
    }

    public void CheckItemSelected()
    {
        Text sButtonText = selectButton.GetComponentInChildren<Text>();
        if (itemBought && this.ToString() != shopScript.currentItem)
        {
            sButtonText.text = "Selecteren";
            selectImage.sprite = deselectSprite;
            sButtonText.color = appelBlauw;
            selectButton.image.sprite = shopScript.buttonLongEnabled;
            selectButton.interactable = true;
            itemSelected = false;
        }
        else if (this.ToString() == shopScript.currentItem)
        {
            sButtonText.text = "Geselecteerd";
            selectImage.sprite = selectSprite;
            selectButton.interactable = false;
            sButtonText.color = Color.white;
            selectButton.image.sprite = shopScript.buttonLongDisabled;
            itemSelected = true;
        }
        else
        {
            sButtonText.text = "Niet in bezit";
            sButtonText.color = Color.white;
            selectImage.sprite = deselectSprite;
            selectButton.image.sprite = shopScript.buttonLongDisabled;
            selectButton.interactable = false;
        }
    }

    public void CheckBuyButtonInteractable()
    {
        //if (shopScript.testCoins >= itemPrice && !itemBought)
        if(GameMaster.totalCoins > itemPrice && !itemBought)
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
        //if (!itemBought && shopScript.testCoins >= itemPrice)
        if(!itemBought && GameMaster.totalCoins > itemPrice)
        {
            shopScript.audSource.PlayOneShot(shopScript.buySound, shopScript.settingsScript.volumeSE);
            //shopScript.testCoins -= itemPrice;
            GameMaster.totalCoins -= itemPrice;
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
            shopScript.audSource.PlayOneShot(shopScript.selectSound, shopScript.settingsScript.volumeSE);
            selectImage.sprite = selectSprite;
            itemSelected = true;
            shopScript.currentItem = this.ToString();
			shopScript.currentValue = this.itemValue;
            shopScript.RefreshShop();
        }
    }
}
