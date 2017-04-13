using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Page : MonoBehaviour {

    //public enum LevelOfPage
    //{
    //    SintAnnastrand, ZwemvijverBoekenberg, Vlaeykensgang
    //};
    //public LevelOfPage levelOfPage;
    private string levelOfPage;
    private GameMaster gmScript;
    private WinGame winPopupScript;

    void Start()
    {
        levelOfPage = SceneManager.GetActiveScene().name;
        gmScript = GameObject.FindWithTag("GameMaster").GetComponent<GameMaster>();
        winPopupScript = GameObject.Find("WinPopup").GetComponent<WinGame>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        winPopupScript.ChangePageSprite();
        gmScript.pageCollected = true;
        //gmScript.UpdatePageCount(levelOfPage);
        Destroy(gameObject);
    }


}
