using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour {

    public GameObject[] itemArray;
    public Text totalCoinUI;
    public Text totalCoinsShop;
    private int itemCount;

	// Use this for initialization
	void Start () {
        
        SetupShop();
	}

    private void SetupShop()
    {
        totalCoinsShop.text = totalCoinUI.text;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<DisguiseItem>().ItemSetup();
        }
    }
}
