using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {

    public enum Direction { Reverse, Normal};
    public Direction currentDirection;
    private Player playerScript;
    private AudioSource reverseSound;

	void Start()
	{
		playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        reverseSound = gameObject.GetComponent<AudioSource>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentDirection == Direction.Reverse)
        {
            reverseSound.Play();
            playerScript.inReverseDirection = true;
        }
        else
        {
            reverseSound.Play();
            playerScript.inReverseDirection = false;
        }
    }
}
