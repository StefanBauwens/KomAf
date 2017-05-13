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

	protected RaycastHit2D hit;
	protected bool startLevel;
	public LevelPoint[] levels;
	protected bool reverseDirection;

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
		this.gameObject.transform.position = new Vector3 (PlayerPrefs.GetInt ("xCoord", 40), PlayerPrefs.GetInt ("yCoord", 32), -1);

		x = (int)this.gameObject.transform.position.x;
		y = (int)this.gameObject.transform.position.y;

		targetPosition = new Vector3 (0,0,0);

		roadColor  = new Color (1, 0, 0);
		grassColor = new Color (0, 0, 0);
		startLevel = false;

		//this might not work if this runs BEFORE the start of tilemapper:
		//levels = FindObjectsOfType(typeof(LevelPoint)) as LevelPoint[];

		reverseDirection = false;
	}
	
	void Update () {
		while (levels.Length==0) {
			levels = FindObjectsOfType(typeof(LevelPoint)) as LevelPoint[];
		}
		if (Input.touchCount>0) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			hit = Physics2D.Raycast(pos, Vector2.zero);
			if (hit != null && hit.collider != null && direction==navigatorDirection.idle && path.Count==0) {
				if (colorArray [(int)hit.collider.transform.position.x + ((int)hit.collider.transform.position.y * worldMap.Map.width)] != grassColor) {
					targetLevel = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, this.gameObject.transform.position.z);

					xx = x;
					yy = y;
					path.Clear ();
					pathLocations.Clear ();
					pathLocations.Add (new Vector3 (xx, yy, this.gameObject.transform.position.z));
					path.Add (navigatorDirection.idle);


					timeToLive = 1500;
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
						lastDirection = directionsToGo [rand.Next (0, directionsToGo.Count)];
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
						foreach (LevelPoint item in levels) {
							if (item.gameObject.transform.position == new Vector3(xx,yy,0) && !item.levelUnlocked) {
								if (targetLevel != new Vector3 (item.gameObject.transform.position.x, item.gameObject.transform.position.y, this.gameObject.transform.position.z)) {
									path.RemoveAt (path.Count - 1);
									pathLocations.RemoveAt (pathLocations.Count - 1);
									xx = (int)pathLocations [pathLocations.Count - 1].x;
									yy = (int)pathLocations [pathLocations.Count - 1].y;	
								}
							}
						}

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
				//YOU HAVE REACHED THE LEVEL you clicked on
				path.Clear();
				pathLocations.Clear ();
				startLevel = true;
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
					checkToStop (0,-1);
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
					checkToStop (-1,0);
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
					checkToStop (0,1);
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
					checkToStop (1,0);
				} else {
					direction = navigatorDirection.idle;
				}
			}
			break;
		}

		if (startLevel && direction == navigatorDirection.idle) {
			startLevel = false;
			if (hit.collider.gameObject.GetComponent<LevelPoint>() != null && hit.collider.gameObject.GetComponent<LevelPoint>().levelUnlocked) { 
				hit.collider.gameObject.GetComponent<LevelPoint> ().HasClickedOnLevel (); //similiar to the effect of pressing a level button
			}
			PlayerPrefs.SetInt ("xCoord", x);
			PlayerPrefs.SetInt ("yCoord", y);
		}

	}

	void checkToStop (int addTox, int addToy)
	{
		foreach (LevelPoint item in levels) {
			if (targetPosition == new Vector3(item.gameObject.transform.position.x, item.gameObject.transform.position.y, targetPosition.z) && !item.levelUnlocked) {
				targetPosition = new Vector3 (targetPosition.x+addTox, targetPosition.y+addToy, targetPosition.z);
				isMoving = false;
				direction = navigatorDirection.idle;
				//Debug.Log ("this happensdsdsd");
			}
		}
	}
}
