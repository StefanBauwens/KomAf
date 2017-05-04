using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorldController : MonoBehaviour {

	protected Vector3 currentVelocity;
	protected float smoothTime;
	protected float maxSpeed;
	protected float xToAdd; //middle of the camera coordinates
	protected float yToAdd;
	public GameObject Player;
	// Use this for initialization
	void Start () {
		currentVelocity = new Vector3 (0,0,0);
		smoothTime = 0.6f;
		maxSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3(Player.transform.position.x + xToAdd , Player.transform.position.y+yToAdd, transform.position.z);
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref currentVelocity, smoothTime, maxSpeed, maxSpeed);
	}
}
