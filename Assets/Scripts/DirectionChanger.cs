using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {

    public enum Direction { Reverse, Normal};
    public Direction currentDirection;
    Player playerScript;

	void Start()
	{
		playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentDirection == Direction.Reverse)
        {
            playerScript.inReverseDirection = true;
        }
        else
        {
            playerScript.inReverseDirection = false;
        }
    }
}
