using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steuerung : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool dFlag = false;
		bool aFlag = false;
		bool upFlag = false;

		if (Input.GetKeyDown(KeyCode.D)) {
			if(!dFlag) {
				dFlag = true;
				MoveForward();
			} else {
				dFlag = false;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			if(!aFlag) {
				aFlag = true;
				MoveBackwards();
			} else {
				aFlag = false;
			}
		}
		
		if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W)) {
			if(!upFlag) {
				upFlag = true;
				Jump();
			} else {
				upFlag = false;
			}
		}
	}

	/*
	*	Den Charakter forwärts bewegen
	 */
	void MoveForward() {
		Debug.Log("Forward");
	}

	/*
	*	Den Charakter rückwärts bewegen
	 */
	void MoveBackwards() {
		Debug.Log("Backwards");
	}

	/*
	*	Den Charakter hüpfen lassen
	 */
	void Jump() {
		Debug.Log("Jump");
	}
}
