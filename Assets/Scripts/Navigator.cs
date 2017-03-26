using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public int x, y;
	public navigatorDirection direction;
	public bool isMoving;
	protected Color[] colorArray;

	public float step;
	protected Vector3 targetPosition;

	protected Color roadColor;
	protected Color grassColor;
	//protected Color stopColor;

	// Use this for initialization
	void Start () {
		step = 0.5f; //speed
		colorArray = worldMap.Map.GetPixels ();
		direction = navigatorDirection.idle;
		isMoving = false;
		x = (int)this.gameObject.transform.position.x;
		y = (int)this.gameObject.transform.position.y;
		targetPosition = new Vector3 (0,0,0);

		roadColor = new Color (1, 0, 0);
		grassColor = new Color (0, 0, 0);
		//ColorUtility.TryParseHtmlString ("#d34aba", out stopColor);
	}
	
	void Update () {
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



		//if (isMoving) {
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
					if (colorArray [x -1 + (y * worldMap.Map.width)] != grassColor && direction!=navigatorDirection.idle) { //HARDCODED
						targetPosition = new Vector3 (this.gameObject.transform.position.x-1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
						isMoving = true;
					} else {
						direction = navigatorDirection.idle;
					}
				}
				break;
			}
		//}
	}
}
