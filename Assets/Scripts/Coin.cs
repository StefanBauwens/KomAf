using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : InteractiveItem {

    public GameMaster gmScript;

    void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.score += 1;
        Destroy(gameObject);
    }

}
