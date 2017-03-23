using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    WinGame winScript;
    GameMaster gmScript;
    protected bool pageFound;

    void Start()
    {
        winScript = GameObject.Find("Canvas").GetComponentInChildren<WinGame>();
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        winScript.ChangePageSprite();
        pageFound = true;
        Destroy(gameObject);
    }

    public void UpdatePageCount()
    {
        if (pageFound)
        {
            gmScript.totalPageCount += 1;
            pageFound = false;
        }
    }

}
