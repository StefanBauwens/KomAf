using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapper : MonoBehaviour {
	public GameObject[] gameObjects;
	public string[] hexColours;
	public Texture2D Map;

	protected Color[] realColours;
	protected GameObject tempObject;
	protected Color[] colorArray;

	// Use this for initialization
	void Start () {
		realColours = new Color[hexColours.Length];
		for (int i = 0; i < hexColours.Length; i++) {
			ColorUtility.TryParseHtmlString ("#"+hexColours[i], out realColours[i]); //convert hex to colour
		}

		colorArray = Map.GetPixels(); //faster than getpixel
		for (int height = 0; height < Map.height; height++) {
			for (int width = 0; width < Map.width; width++) {
				tempObject = null;

				for (int i = 0; i < realColours.Length; i++) { //check every colour
					if (colorArray [width + (height * Map.width)] == realColours[i]) {
						tempObject = gameObjects[i];
						break;
					}
				}

				if (tempObject != null) {
					GameObject newInstant = Instantiate (tempObject, new Vector2 (width, height), Quaternion.identity);
					newInstant.name = tempObject.name;
				} else {
					if (colorArray [width + (height * Map.width)] != new Color(1f,1f,1f)) {
						Debug.Log (colorArray [width + (height * Map.width)]);

					}
				}
			}
		}
	}
}
