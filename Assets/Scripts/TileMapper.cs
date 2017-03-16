using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapper : MonoBehaviour {
	public GameObject RedObject; //please use objects with all the same width & height for no problems
	public GameObject GreenObject;
	public GameObject defaultObject;
	public Texture2D Map;

	protected GameObject tempObject;
	//protected const int SPRITEWIDTH = 16;
	protected Color RED = new Color (1f, 0, 0);
	protected Color GREEN = new Color (0, 1f, 0);
	protected Color[] colorArray;

	// Use this for initialization
	void Start () {
		colorArray = Map.GetPixels (); //faster than getpixel
		for (int height = 0; height < Map.height; height++) {
			for (int width = 0; width < Map.width; width++) {
				
				if (colorArray[width+ (height*Map.width)] == RED) {
					tempObject = RedObject;
				} else if (colorArray[width+ (height*Map.width)] == GREEN) {
					tempObject = GreenObject;
				} else {
					tempObject = defaultObject;
				}

				Instantiate(tempObject, new Vector2(width, height), Quaternion.identity);
				Debug.Log (colorArray[width+ (height*Map.width)]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
