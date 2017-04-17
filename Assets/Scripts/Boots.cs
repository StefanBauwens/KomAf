using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Powerup {

    Player playerScript;
    public float newJumpHeight = 8f;
    protected float extraJumpTime = 5f;
    protected Renderer renderer;
    protected Collider2D collider;
    protected AudioSource jumpHigherSound;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
		playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        jumpHigherSound = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        jumpHigherSound.Play();
        renderer.enabled = false; // object not visible for camera
        collider.enabled = false; // --> player can't interact with object
        StartCoroutine(JumpHigher());
    }

    IEnumerator JumpHigher()
    {
        playerScript.jumpHeight = newJumpHeight;
        yield return new WaitForSeconds(extraJumpTime);
        playerScript.jumpHeight = 5f;
        Destroy(gameObject);
    }

}
