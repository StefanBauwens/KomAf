using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    public WinGame winScript;
    public GameMaster gmScript;
    protected bool pageFound;

    private void OnTriggerEnter2D(Collider2D collision)
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
