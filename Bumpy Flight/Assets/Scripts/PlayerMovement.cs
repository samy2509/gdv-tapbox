using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float mSpeed = 10.0f;
    private float gravity = 14.0f;
    private float jumpForce = 14.0f;
    private Vector3 moveDi = Vector3.zero;

    void Start () {
        mSpeed = 7.0f;
	}

	void Update () {
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        if (controller.isGrounded) {
            //Bewegung nach rechts, links
            moveDi = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDi = transform.TransformDirection(moveDi);
            moveDi *= mSpeed;

            if (Input.GetButtonDown("Jump")) {
                moveDi.y = jumpForce;
            }
            
        }
        //fallen
        moveDi.y -= gravity * Time.deltaTime;
        controller.Move(moveDi * Time.deltaTime);
    }

}
