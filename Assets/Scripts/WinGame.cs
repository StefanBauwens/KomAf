using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame :MonoBehaviour{

    public Sprite pageFoundSprite;
    public Image pageImage;
    GameMaster gmScript;
    public Button menuButton;
    public Button restartButton;
    SceneController sceneConScript;

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        sceneConScript = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();

        menuButton.onClick.AddListener(()=> sceneConScript.LoadAntwerpMap());
        restartButton.onClick.AddListener(()=> sceneConScript.RestartLevel());
    }

    public void ChangePageSprite()
    {
        pageImage.GetComponent<Image>().sprite = pageFoundSprite;
    }
}
