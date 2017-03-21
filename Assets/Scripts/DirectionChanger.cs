using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour {

    public enum Direction { Reverse, Normal};
    public Direction currentDirection;
    public Player playerScript;

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
