using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {

    public enum Direction { Reverse, Normal};
    public Direction currentDirection;
    private Player playerScript;
    private Settings settingsScript;
    private AudioSource audSource;
    public AudioClip reverseSound;

	void Start()
	{
		playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        settingsScript = GameObject.FindGameObjectWithTag("SettingsCanvas").GetComponent<Settings>();
        audSource = gameObject.GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.collider.name == "Player") {
			if (currentDirection == Direction.Reverse) {
				playerScript.inReverseDirection = true;
				audSource.PlayOneShot(reverseSound, settingsScript.volumeSE);
			} else {
				playerScript.inReverseDirection = false;
				audSource.PlayOneShot(reverseSound, settingsScript.volumeSE);

			}
		}
    }
}
