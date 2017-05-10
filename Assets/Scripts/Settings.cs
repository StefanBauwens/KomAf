using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    private static Settings instanceRef;
    public CanvasGroup settingsCanvasGroup;
    public float volumeSE;
    public float volumeM;
    public Button infoButton;
    public Button websiteButton;
    public Button settingsButtonCanvas;
    public AudioClip buttonSound;
    public CanvasGroup infoCanvas;
    protected AudioSource audSource;

    public Image imageSE;
    public Image imageM;
    public Sprite soundSE;
    public Sprite soundM;
    public Sprite muteSE;
    public Sprite muteM;

    void Awake()
    {
        if (instanceRef == null)
        {
            
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audSource = GetComponent<AudioSource>();     
        volumeSE = 0.5f;
        volumeM = 0.5f;
    }
    

    public void ChangeVolumeSoundEffects(float newVolumeSE)
    {
        volumeSE = newVolumeSE/4;
        ChangeMuteSprite();
        Debug.Log("SE: " + volumeSE);
        
    }

    public void ChangeVolumeMusic(float newVolumeM)
    {
        volumeM = newVolumeM/4;
        ChangeMuteSprite();
        Debug.Log("M: " + volumeM);
    }

    public void OpenSettings()
    {
        PlayButtonSound(audSource);
        settingsCanvasGroup.alpha = 1;
        settingsCanvasGroup.interactable = true;
        settingsCanvasGroup.blocksRaycasts = true;
    }

    public void CloseSettings()
    {
        PlayButtonSound(audSource);
        settingsCanvasGroup.alpha = 0;
        settingsCanvasGroup.interactable = false;
        settingsCanvasGroup.blocksRaycasts = false;
    }

    public void PlayButtonSound(AudioSource audS)
    {
        audS.PlayOneShot(buttonSound, volumeSE);
    }

    public void OpenInfoCanvas()
    {
        PlayButtonSound(audSource);
        infoCanvas.alpha = 1;
        infoCanvas.interactable = true;
        infoCanvas.blocksRaycasts = true;
    }

    public void CloseInfoCanvas()
    {
        PlayButtonSound(audSource);
        infoCanvas.alpha = 0;
        infoCanvas.interactable = false;
        infoCanvas.blocksRaycasts = false;
    }


    public void ChangeMuteSprite()
    {
        if (volumeSE == 0)
        {
            imageSE.sprite = muteSE;
        }
        else
        {
            imageSE.sprite = soundSE;
        }

        if (volumeM == 0)
        {
            imageM.sprite = muteM;
        }
        else
        {
            imageM.sprite = soundM;
        }
    }
}
