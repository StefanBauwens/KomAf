using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    GameMaster gmScript;

	void Start()
	{
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.AddCollectedCoinPosition(gameObject.transform.position);
        gmScript.coinsCollectedInLevel += 1;
        Destroy(gameObject);
    }

}
