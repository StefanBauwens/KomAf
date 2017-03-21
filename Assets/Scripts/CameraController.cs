using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Player playerScript;
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public float addToX = 0;
    public float addToY = 0;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    void FixedUpdate()
    {
        
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + addToX, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + addToY, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, -11);

        if (playerScript.isAgainstObject)
        {
            if(playerScript.inReverseDirection)
            {
                addToX -= 0.05f;
            }
            else
            {
                addToX += 0.05f;
            }
        }
        else
        {
            addToY = Mathf.Lerp(addToY, 0, 0.5f * Time.deltaTime);
            addToX = Mathf.Lerp(addToX, 0, 0.5f * Time.deltaTime);
        }
        
    }

}
