using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour {

    public Player player;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            player.isAgainstWall = true;
            Debug.Log("Is against wall");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.isAgainstWall = false;
        Debug.Log("Not against wall!");
    }
}
