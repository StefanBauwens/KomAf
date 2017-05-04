using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Player playerScript;
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    protected float addToX = 0;
    protected float addToY = 0;

	public float speedCamera = 0.03f;
	protected float speedBackToNormal = 1f;

    // Use this for initialization
    void Start () {
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
				addToX -= speedCamera;
            }
            else
            {
				addToX += speedCamera;
            }
        }
        else
        {
			addToY = Mathf.Lerp(addToY, 0, speedBackToNormal * Time.deltaTime);
			addToX = Mathf.Lerp(addToX, 0, speedBackToNormal * Time.deltaTime);
        }
        
    }

}
