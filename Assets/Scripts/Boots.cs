using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour {

    Player playerScript;
    public float newJumpHeight = 11f;
	protected float oldJumpHeight;
    protected float extraJumpTime = 5f;
    protected Renderer renderer;
    protected Collider2D collider;

    protected AudioSource audSource;
    public AudioClip jumpHigherSound;
    protected Settings settingsScript;

	public Circle timer;

	// Use this for initialization
	void Start () {

        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
		playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
		oldJumpHeight = playerScript.smallJump;
        audSource = gameObject.GetComponent<AudioSource>();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
    }

	void Update()
	{
		if (timer == null) {
			timer = GameObject.FindGameObjectWithTag ("Counter").GetComponentInChildren<Circle>();
		}
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        renderer.enabled = false; // object not visible for camera
        collider.enabled = false; // --> player can't interact with object
		timer.StartTimer();
		StopCoroutine("JumpHigher");
		StartCoroutine("JumpHigher");

		audSource.PlayOneShot(jumpHigherSound, settingsScript.volumeSE);
    }

    IEnumerator JumpHigher()
    {
		for (int i = 0; i < extraJumpTime; i++) {
        	playerScript.jumpHeight = newJumpHeight;
			yield return new WaitForSeconds(1);
		}

		playerScript.jumpHeight = oldJumpHeight;
		renderer.enabled = true;
		collider.enabled = true;

    }

}
