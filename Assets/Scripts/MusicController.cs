using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour {

    public AudioClip musicLevel;
    public AudioClip musicAntwerpmap;
    public PopupController popupConScript;
    protected Settings settingsScript;
    protected AudioSource audSource;
    protected string antwerpMap = "AntwerpMap2";

	// Use this for initialization
	void Start () {
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        audSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
	}

    void Update()
    {
        audSource.volume = settingsScript.volumeM;
    }

    void PlayBackgroundMusic()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name == antwerpMap)
        {
            Debug.Log("play antwerpmap");
            audSource.clip = musicAntwerpmap;
            audSource.Play();
        }
        else
        {
            Debug.Log("play level");
            audSource.clip = musicLevel;
            audSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        audSource.Stop();
    }
	
}
