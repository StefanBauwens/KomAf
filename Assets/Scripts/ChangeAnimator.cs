using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeAnimator : MonoBehaviour {
	public string[] disguisedItems;
	public RuntimeAnimatorController[] disguisedAnimators;

	protected GameObject player;
	protected GameMaster gmScript;
	protected bool hasRun;

	// Use this for initialization
	void Start () {
		gmScript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
		player = GameObject.FindGameObjectWithTag ("Player");
		hasRun = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasRun) {
			hasRun = true;
			string currentDisguise = gmScript.GetCurrentDisguise ();
			if (currentDisguise != "noDisguiseSelected") {
				currentDisguise = currentDisguise.Substring(0,currentDisguise.IndexOf("(")-1); //this removes the "(DisguisedItem)" from the string
				player.GetComponent<Animator> ().runtimeAnimatorController = disguisedAnimators[Array.IndexOf (disguisedItems, currentDisguise)];
			}
		}
	}
}
