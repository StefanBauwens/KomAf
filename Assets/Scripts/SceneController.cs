using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    LevelController levelController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadAntwerpMap()
    {
        SceneManager.LoadScene("AntwerpMap");
        //levelController.CheckLevelUnlocked();
    }
}
