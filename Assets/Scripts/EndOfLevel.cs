﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : PopupManager {
    protected byte aCount;
    protected int highscore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinPopup();
    }
}
