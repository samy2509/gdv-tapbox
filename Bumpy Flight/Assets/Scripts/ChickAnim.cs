using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickAnim : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			anim.Play("Fly", -1, 0f);
		}


		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
			anim.Play("Walk", -1, 0f);
		}
		if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
			anim.Play("Idle_A", -1, 0f);
		}


		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			anim.Play("Roll", -1, 0f);
		}
		if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
			anim.Play("Idle_A", -1, 0f);
		}


		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			anim.Play("Clicked", -1, 0f);
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			anim.Play("Idle_A", -1, 0f);
		}
	}
}
