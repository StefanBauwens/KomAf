using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour {
	public GameObject[] colorPrefabs;
	public float waitTime;
	protected int iterator;
	protected GameObject colorfg;

	// Use this for initialization
	void Start () {
		iterator = -1;
		StartCoroutine (ChangeColor());
	}

	protected IEnumerator ChangeColor()
	{
		while (true) { 
			if (iterator < colorPrefabs.Length - 1) {
				if (colorfg != null) {
					Destroy (colorfg, 0.0f);
				}
				iterator++;
				colorfg = Instantiate (colorPrefabs [iterator], new Vector3 (-0.5f, -0.5f, -1), Quaternion.identity);
			} else {
				iterator = -1;
			}
			Debug.Log ("couroutine running");
			yield return new WaitForSeconds(waitTime);

		}
		yield return null;
	}
}
