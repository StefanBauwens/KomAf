using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    GameMaster gmScript;

	void Start()
	{
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.coinsCollectedInLevel += 1;
        Destroy(gameObject);
    }

}
