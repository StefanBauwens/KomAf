using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private GameMaster gmScript;
    private AudioSource audSource;
    private AudioClip coinSound;
    private Renderer renderer;
    private Collider2D collider;

	void Start()
	{
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        audSource = gameObject.GetComponent<AudioSource>();
        renderer = gameObject.GetComponent<Renderer>();
        collider = gameObject.GetComponent<Collider2D>();
        coinSound = audSource.clip;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") {
            
			audSource.PlayOneShot(coinSound);
			renderer.enabled = false;
			collider.enabled = false;
			gmScript.AddCollectedCoinPosition(gameObject.transform.position);
			gmScript.coinsCollectedInLevel += 1;
			Destroy(gameObject, coinSound.length);
		} 
    }

}
