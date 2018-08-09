using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    public float mSpeed = 10.0f;
    private float gravity = 44.0f;
    private float jumpForce = 24.0f;
    private float velocity = 0;
    private bool inputJump;

    void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        mSpeed = 7.0f;
	}

	void Update () {
        InputCheck();
        Move();
        //SetAnimation();
    }
    void InputCheck() {
        velocity = Input.GetAxis("Horizontal") * mSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputJump = true;
        }
        else {
            inputJump = false;
        }
    }
    void Move() {
        if (controller.isGrounded) {
            if (inputJump) {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; 
        controller.Move(moveDirection * Time.deltaTime);
        
        
    }

   /* void SetAnimation() {
    }Später für animatin beim springen 
    */
}
