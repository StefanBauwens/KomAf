using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// player in Antwerp map

public enum navigatorDirection {
	up,
	right,
	down,
	left,
	idle
}

public class Navigator : MonoBehaviour {

	public TileMapper worldMap;

	protected int x, y, xx, yy;// prevxx, prevyy;
	public navigatorDirection direction;
	public bool isMoving;
	protected Color[] colorArray;

	public float step;
	protected Vector3 targetPosition;
	protected Vector3 targetLevel; //the level you clicked on on the map and want to move to

	protected Color roadColor;
	protected Color grassColor;

	protected List<navigatorDirection> directionsToGo;
	protected List<navigatorDirection> path;
	protected List<Vector3> pathLocations;

	protected navigatorDirection lastDirection;

	protected int counter;
	protected int timeToLive;

	public navigatorDirection[] directionsOfPath;
	public Vector3[] locationsPath;

	// Use this for initialization
	void Start () {
		directionsToGo = new List<navigatorDirection> ();
		path		   = new List<navigatorDirection> ();
		pathLocations  = new List<Vector3> ();

		lastDirection     = navigatorDirection.idle;
		counter = 0;
		step = 0.5f; //speed

		colorArray = worldMap.Map.GetPixels ();
		direction  = navigatorDirection.idle;
		isMoving   = false;

		x = (int)this.gameObject.transform.position.x;
		y = (int)this.gameObject.transform.position.y;

		targetPosition = new Vector3 (0,0,0);

		roadColor  = new Color (1, 0, 0);
		grassColor = new Color (0, 0, 0);
	}
	
	void Update () {
		if (Input.touchCount>0) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
			if (hit != null && hit.collider != null && direction==navigatorDirection.idle && path.Count==0) {
				if (colorArray [(int)hit.collider.transform.position.x + ((int)hit.collider.transform.position.y * worldMap.Map.width)] != grassColor) {
					targetLevel = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, this.gameObject.transform.position.z);

					xx = x;
					yy = y;
					path.Clear ();
					pathLocations.Clear ();
					pathLocations.Add (new Vector3 (xx, yy, this.gameObject.transform.position.z));
					path.Add (navigatorDirection.idle);

					//CLEAN UP CODE LATER

					//MAKE IT CHECK THAT NONE OF THOSE POSITIONS ALREADY HAVE BEEN  WALKED ON, IF SO REMOVE EVERYTHING TILL POINT
					timeToLive = 1000;
					while (new Vector3(xx, yy, this.gameObject.transform.position.z) != targetLevel && timeToLive>0) {
						directionsToGo.Clear ();
						if (colorArray [xx + 1 + (yy * worldMap.Map.width)] == roadColor) {
							if (targetLevel.x > xx) { //add more chance if targetlevel is in this direction
								directionsToGo.Add (navigatorDirection.right); 
							}
							directionsToGo.Add (navigatorDirection.right);
						}
						if (colorArray [xx - 1 + (yy * worldMap.Map.width)] == roadColor) {
							if (targetLevel.x < xx) { //add more chance if targetlevel is in this direction
								directionsToGo.Add (navigatorDirection.left); 
							}
							directionsToGo.Add (navigatorDirection.left);
						}
						if (colorArray [xx + ((yy + 1) * worldMap.Map.width)] == roadColor) {
							if (targetLevel.y > yy) { //add more chance if targetlevel is in this direction
								directionsToGo.Add (navigatorDirection.up); 
							}
							directionsToGo.Add (navigatorDirection.up);
						}
						if (colorArray [xx + ((yy - 1) * worldMap.Map.width)] == roadColor) {
							if (targetLevel.y < yy) { //add more chance if targetlevel is in this direction
								directionsToGo.Add (navigatorDirection.down); 
							}
							directionsToGo.Add (navigatorDirection.down);
						}

						System.Random rand = new System.Random();
						lastDirection = directionsToGo[rand.Next (0, directionsToGo.Count)];
						path.Add (lastDirection);

						switch (lastDirection) {
						case navigatorDirection.up:
							while (colorArray [xx + (( yy + 1 ) * worldMap.Map.width)] == roadColor) {
								yy++;
							}
							if (colorArray [xx + (( yy + 1 ) * worldMap.Map.width)] != roadColor && colorArray [xx + (( yy + 1 ) * worldMap.Map.width)]!=grassColor) {
								yy++;
							}
							break;
						case navigatorDirection.right:
							while (colorArray [xx + 1 + (yy * worldMap.Map.width)] == roadColor) {
								xx++;
							}
							if (colorArray [xx + 1 + (yy * worldMap.Map.width)] != roadColor && colorArray [xx + 1 + (yy * worldMap.Map.width)]!=grassColor) {
								xx++;
							}
							break;
						case navigatorDirection.down:
							while (colorArray [xx + (( yy - 1 ) * worldMap.Map.width)] == roadColor) {
								yy--;
							}
							if (colorArray [xx + (( yy - 1 ) * worldMap.Map.width)] != roadColor && colorArray [xx + (( yy - 1 ) * worldMap.Map.width)]!=grassColor) {
								yy--;
							}
							break;
						case navigatorDirection.left:
							while (colorArray [xx - 1 + (yy * worldMap.Map.width)] == roadColor) {
								xx--;
							}
							if (colorArray [xx - 1 + (yy * worldMap.Map.width)] != roadColor && colorArray [xx - 1 + (yy * worldMap.Map.width)]!=grassColor) {
								xx--;
							}
							break;
						}
						timeToLive--;
						pathLocations.Add (new Vector3 (xx, yy, this.gameObject.transform.position.z));
					}
						
					if (timeToLive==0) {
						Debug.Log ("Timetolive up!");
						path.Clear ();
						pathLocations.Clear ();
					}
						
					//The following removes the navigator from going back and forth: Is no longer neccesary with the code under it, but still fancy

					/*for (int i = 0; i < (path.Count)&&path.Count>1;i++) {
						if (i==0) {
							i = 1;
						}
						if (path [i] == (navigatorDirection)(((int)path [i - 1] + 2) % 4)) {
							path.RemoveAt (i - 1);
							path.RemoveAt (i - 1);
							i -= 2;
						}
					}*/


					//The following checks the list of calculated positions and sees if the same position is found twice or more in the list
					//If so, this means that the player will somewhere be going in circles. So we eliminate all the positions betweeen these
					//duplicates so that we get a more optimised route.

					List<Vector3> tempList 			  = new List<Vector3> ();
					List<navigatorDirection> tempPath = new List<navigatorDirection> ();

					for (int i = 0; i < pathLocations.Count; i++) {
						if (tempList.Contains (pathLocations [i])) {
							int indexNr = tempList.LastIndexOf (pathLocations [i]);
							tempList.RemoveRange (indexNr + 1, tempList.Count - indexNr - 1);
							tempPath.RemoveRange (indexNr + 1, tempPath.Count - indexNr - 1);
						} else {
							tempList.Add(pathLocations[i]);
							tempPath.Add(path[i]);
						}
					}
						
					path = tempPath;
					locationsPath = tempList.ToArray ();
						
					directionsOfPath = path.ToArray(); //this shows the calculated route in the inspector and is more for debugging purposes

					counter = 0;
				}
			}
		}




		if (direction==navigatorDirection.idle) {
			targetPosition = new Vector3 (0, 0, 0);
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				direction = navigatorDirection.up;
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow)) {
				direction = navigatorDirection.right;
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow)) {
				direction = navigatorDirection.down;
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				direction = navigatorDirection.left;
			}
		}

		//This checks if a path has been calculated and will move you step by step(counter goes up each time till the path has been completed)
		if (path.Count>0 && direction==navigatorDirection.idle) {
			direction = path [counter];
			counter++;
			if (path.Count==counter) {
				path.Clear();
				pathLocations.Clear ();
			}
		}
			
		switch (direction) {
		case navigatorDirection.up:
			if (isMoving) {
				this.gameObject.transform.position = Vector3.Lerp (this.gameObject.transform.position, targetPosition, step/Vector3.Distance(this.gameObject.transform.position, targetPosition));
				if (this.gameObject.transform.position == targetPosition) {
					isMoving = false;
					y++;
					if (colorArray [x + (y * worldMap.Map.width)] !=roadColor) {
						direction = navigatorDirection.idle;
					}
				}
			}

			if (!isMoving) {
				if (colorArray [x + ((y + 1) * worldMap.Map.width)] != grassColor && direction != navigatorDirection.idle) { //HARDCODED
					targetPosition = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
					isMoving = true;
				} else {
					direction = navigatorDirection.idle;
				}
			}
			break;
		case navigatorDirection.right:
			if (isMoving) {
				this.gameObject.transform.position = Vector3.Lerp (this.gameObject.transform.position, targetPosition, step/Vector3.Distance(this.gameObject.transform.position, targetPosition));
				if (this.gameObject.transform.position == targetPosition) {
					isMoving = false;
					x++;
					if (colorArray [x + (y * worldMap.Map.width)] !=roadColor) {
						direction = navigatorDirection.idle;
					}
				}
			}

			if (!isMoving) {
				if (colorArray [x + 1 + (y * worldMap.Map.width)] != grassColor && direction!=navigatorDirection.idle) { //HARDCODED
					targetPosition = new Vector3 (this.gameObject.transform.position.x+1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					isMoving = true;
				} else {
					direction = navigatorDirection.idle;
				}
			}
			break;
		case navigatorDirection.down:
			if (isMoving) {
				this.gameObject.transform.position = Vector3.Lerp (this.gameObject.transform.position, targetPosition, step/Vector3.Distance(this.gameObject.transform.position, targetPosition));
				if (this.gameObject.transform.position == targetPosition) {
					isMoving = false;
					y--;
					if (colorArray [x + (y * worldMap.Map.width)] !=roadColor) {
						direction = navigatorDirection.idle;
					}
				}
			}

			if (!isMoving) {
				if (colorArray [x + ((y-1) * worldMap.Map.width)] !=grassColor && direction!=navigatorDirection.idle) { //HARDCODED
					targetPosition = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1, this.gameObject.transform.position.z);
					isMoving = true;
				} else {
					direction = navigatorDirection.idle;
				}
			}
			break;
		case navigatorDirection.left:
			if (isMoving) {
				this.gameObject.transform.position = Vector3.Lerp (this.gameObject.transform.position, targetPosition, step/Vector3.Distance(this.gameObject.transform.position, targetPosition));
				if (this.gameObject.transform.position == targetPosition) {
					isMoving = false;
					x--;
					if (colorArray [x + (y * worldMap.Map.width)] !=roadColor) {
						direction = navigatorDirection.idle;
					}
				}
			}

			if (!isMoving) {
				if (colorArray [x - 1 + (y * worldMap.Map.width)] != grassColor && direction!=navigatorDirection.idle) { //HARDCODED
					targetPosition = new Vector3 (this.gameObject.transform.position.x-1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					isMoving = true;
				} else {
					direction = navigatorDirection.idle;
				}
			}
			break;
		}

	}
}
