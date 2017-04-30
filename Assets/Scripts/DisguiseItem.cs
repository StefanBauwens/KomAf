using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ItemState
{
    InShop, Bought, Selected, NotSelected
};

public class DisguiseItem : MonoBehaviour {

    public Image disguiseImage;
    public Text disguiseName;
    public Text disguiseDescription;
    public Text disguisePrice;
    public GameObject button;
    private Button buySelectButton;
    private ItemState currentState;

    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int price;

	// Use this for initialization
	void Start () {

	}
	
	public void ItemSetup()
    {
        disguiseImage.sprite = itemSprite;
        disguiseName.text = itemName;
        disguiseDescription.text = itemDescription;
        disguisePrice.text = price.ToString();
        buySelectButton = button.GetComponent<Button>();
        CheckState();
    }

    public void CheckState()
    {
        if(currentState == ItemState.InShop)
        {
            buySelectButton.GetComponentInChildren<Text>().text = "Kopen";
        }
        else if (currentState == ItemState.Bought)
        {
            buySelectButton.GetComponentInChildren<Text>().text = "Gekocht";
        }
        else if (currentState == ItemState.NotSelected)
        {
            buySelectButton.GetComponentInChildren<Text>().text = "Aandoen";
        }
        else if (currentState == ItemState.Selected)
        {
            buySelectButton.GetComponentInChildren<Text>().text = "Uitdoen";
        }
    }
}
