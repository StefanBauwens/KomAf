using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Powerup {

    Player playerScript;
    public float newJumpHeight = 11f;
	protected float oldJumpHeight;
    protected float extraJumpTime = 5f;
    protected Renderer renderer;
    protected Collider2D collider;

    protected AudioSource audSource;
    public AudioClip jumpHigherSound;
    protected Settings settingsScript;

	protected bool restart;

	protected IEnumerator routine;

	public Circle timer;
	//protected Coroutine lastRoutine;

	// Use this for initialization
	void Start () {
		routine = JumpHigher ();
		restart = false;
		//lastRoutine = null;
		//newJumpHeight = 11f;
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
		//StopAllCoroutines ();
		StopCoroutine(routine);
		StartCoroutine(routine);

		audSource.PlayOneShot(jumpHigherSound, settingsScript.volumeSE);
    }

    IEnumerator JumpHigher()
    {
		restart = false;
        playerScript.jumpHeight = newJumpHeight;
        yield return new WaitForSeconds(extraJumpTime);

		playerScript.jumpHeight = oldJumpHeight;
		renderer.enabled = true;
		collider.enabled = true;

    }

}
