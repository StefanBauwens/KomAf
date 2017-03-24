using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {

    public enum LevelOfPage
    {
        MAS, ChinaTown, Kathedraal
    };
    public LevelOfPage levelOfPage;
    WinGame winScript;
    GameMaster gmScript;
    public LevelController levelConScript;

    void Start()
    {
        winScript = GameObject.Find("Canvas").GetComponentInChildren<WinGame>();
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gmScript.UpdatePageCount(levelOfPage.ToString());
        Destroy(gameObject);
    }


}
