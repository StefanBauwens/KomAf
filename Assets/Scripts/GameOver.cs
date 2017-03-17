using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : Popup {

    public bool isDead = false;
    public GameObject gameOver;

    void GameOverPopup()
    {
        if (isDead)
        {
            gameOver.SetActive(true);
        }
    }

}
