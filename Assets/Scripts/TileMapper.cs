using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileMapper : MonoBehaviour {
	public GameObject[] gameObjects;
	public string[] hexColours;
	public Texture2D Map;
	public Texture2D SecretMap;

    protected Color[] realColours;
	protected GameObject tempObject;
	protected Color[] colorArray;
    protected GameMaster gmScript;
    protected string level;


	// Use this for initialization
	void Start () {
        gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if((SceneManager.GetActiveScene().name) != "AntwerpMap2") //hardcoded
        {
            level = SceneManager.GetActiveScene().name;
            gmScript.GetCollectedCoinPositions(level);
        }

		if (SecretMap != null &&  PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_secret", 0)==1) {
			Map = SecretMap;
		}

		realColours = new Color[hexColours.Length];
		for (int i = 0; i < hexColours.Length; i++) {
			ColorUtility.TryParseHtmlString ("#"+hexColours[i], out realColours[i]); //convert hex to colour
		}
			
		colorArray = Map.GetPixels(); //faster than getpixel
		for (int height = 0; height < Map.height; height++) { //draws the map
			for (int width = 0; width < Map.width; width++) {
				tempObject = null;

				for (int i = 0; i < realColours.Length; i++) { //check every colour
					if (colorArray [width + (height * Map.width)] == realColours[i]) {
						tempObject = gameObjects[i];
						break;
					}
				}

				if (tempObject != null)
                { 
                    GameObject newInstant = Instantiate(tempObject, new Vector2(width, height), Quaternion.identity);
                    newInstant.name = tempObject.name;

                    if (newInstant.name == "coin")
                    {
                        gmScript.maxCoins++;
                        if (gmScript.collectedCoinsPos.Contains(newInstant.transform.position))// coin already collected --> delete gameobject
                        {
                            Destroy(newInstant);
                        }
                    }

                    if(newInstant.name == "Page")
                    {
                        if(gmScript.CheckPageCollected(level))
                        {
                            Destroy(newInstant);
                        }
                    }
                }
                else
                {
					if (colorArray [width + (height * Map.width)] != new Color(1f,1f,1f)) {
						Debug.Log (colorArray [width + (height * Map.width)]);

					}
			    }
                
			}
		}
        // after drawing done
        if((SceneManager.GetActiveScene().name) != "AntwerpMap2") // hardcoded
        {
            gmScript.SetMaxCoins();
        }
        
	}

    
}
