﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    public enum LevelOfPage
    {
        SintAnnastrand, ZwemvijverBoekenberg, Vlaeykensgang
    };
    public LevelOfPage levelOfPage;
    private GameMaster gmScript;

    void Start()
    {
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.UpdatePageCount(levelOfPage.ToString());
        Destroy(gameObject);
    }


}
