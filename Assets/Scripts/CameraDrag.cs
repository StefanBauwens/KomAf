﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {

	public float dragSpeed = 2;
	private Vector3 dragOrigin;
	public bool isDragging;

	protected CanvasGroup gate15Canvas;
	protected CanvasGroup ShopCanvas;

	void Start()
	{
		isDragging = false;
		gate15Canvas = GameObject.FindGameObjectWithTag ("Gate15").GetComponent<CanvasGroup>();
		ShopCanvas = GameObject.FindGameObjectWithTag ("Shop").GetComponent<CanvasGroup>();
	}

	void Update()
	{
		if (gate15Canvas.interactable || ShopCanvas.interactable) {
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			isDragging = true;
			return;
		}

		if (Input.GetMouseButtonUp(0)) {
			isDragging = false;
			//dragOrigin = Input.mousePosition;
		}

		if (!Input.GetMouseButton(0)) return;

		Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition-dragOrigin);
		Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);

		transform.Translate(move, Space.World);  
		//transform.position += move;

		dragOrigin = Input.mousePosition;
	}
}
