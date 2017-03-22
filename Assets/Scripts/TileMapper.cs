using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapper : MonoBehaviour {
	//public GameObject RedObject; //please use objects with all the same width & height for no problems
	//public GameObject GreenObject;

	public GameObject[] gameObjects;
	public Vector3[] Colours;//X = r Y = g Z = b
	public string[] hexColours;
	//public GameObject defaultObject;
	public Texture2D Map;

	protected Color[] realColours;

	protected GameObject tempObject;
	//protected Color RED = new Color (1f, 0, 0);
	//protected Color GREEN = new Color (0, 1f, 0);
	protected Color[] colorArray;

	// Use this for initialization
	void Start () {
		//converts the vector3's to colours:
		realColours = new Color[Colours.Length];
		for (int i = 0; i < hexColours.Length; i++) {
			//realColours [i] = new Color (Colours [i].x/255f, Colours [i].y/255f, Colours [i].z/255f);
			ColorUtility.TryParseHtmlString ("#"+hexColours[i], out realColours[i]);
		}

		colorArray = Map.GetPixels(); //faster than getpixel
		for (int height = 0; height < Map.height; height++) {
			for (int width = 0; width < Map.width; width++) {
				tempObject = null;
				for (int i = 0; i < realColours.Length; i++) {
					if (colorArray [width + (height * Map.width)] == realColours[i]) {
						tempObject = gameObjects[i];
					}
				}
				/*if (colorArray[width+ (height*Map.width)] == RED) {
					tempObject = RedObject;
				} else if (colorArray[width+ (height*Map.width)] == GREEN) {
					tempObject = GreenObject;
				} else {
					//tempObject = defaultObject;
					tempObject = null;
				}*/

				if (tempObject != null) {
					Instantiate (tempObject, new Vector2 (width, height), Quaternion.identity);
				} else {
					if (colorArray [width + (height * Map.width)] != new Color(1f,1f,1f)) {
						Debug.Log (colorArray [width + (height * Map.width)]);

					}
				}
			}
		}
	}
}
