using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : InteractiveItem{

    public WinGame winScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winScript.ChangePageSprite();
        Destroy(gameObject);
    }

}
