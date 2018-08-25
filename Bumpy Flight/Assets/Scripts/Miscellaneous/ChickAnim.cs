using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickAnim : MonoBehaviour {

	public Animator anim;
	public CharacterController controller;

	void Start () {
		anim 			= 			GetComponent<Animator>();
	}
	
	void Update () {
		// Vorwärts laufen
		if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D)) && controller.isGrounded == true) {
			anim.Play("Walk");
		}
		// Vorwärts laufen Ende
		if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
			anim.Play("Idle_A", -1, 0f);
		}


		// Ei abwerfen
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
			anim.Play("Roll", -1, 0f);
		}
		// Ei abwerfen Ende
		if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				anim.Play("Walk", -1, 0f);
			} else {
				anim.Play("Idle_A", -1, 0f);
			}
		}


		// Zurück laufen
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A)) {
			anim.Play("Walk");
		}
		// Zurück laufen Ende
		if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
			anim.Play("Idle_A", -1, 0f);
		}


		// Springen
		// if(Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W)) {
		// 	anim.Play("Fly");
		// }
		// Springen Ende
		// if(Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.W)) {
		// 	if(controller.isGrounded && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
		// 		anim.Play("Walk", -1, 0f);
		// 	} else {
		// 		anim.Play("Idle_A", -1, 0f);
		// 	}
		// }

		if(controller.isGrounded == false && !anim.GetCurrentAnimatorStateInfo(0).IsName("Roll")) {
			anim.Play("Fly");
		}
	}
}