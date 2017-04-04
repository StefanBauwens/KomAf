using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour {

    public Player player;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            player.isAgainstObject = true;
			//Debug.Log ("Isagainst");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.isAgainstObject = false;
    }
}
