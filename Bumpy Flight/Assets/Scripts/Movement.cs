using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float mSpeed = 10f;
    private float gravity = 44.0f;
    private float jumpForce = 14.0f;
    private Vector3 moveDi = Vector3.zero;
    CharacterController controller;
    private float zStart;
    private int direction;
    private int rotate;

    void Start () {
        controller = gameObject.GetComponent<CharacterController>();
        mSpeed = 7.0f;
        zStart = gameObject.transform.position.z;
        direction = -1;
        rotate = 1;
	}

	void Update () {
        if( gameObject != null ) {
            // if( rotate != direction ) {
            //     TurnAround();
            // } else {
                moveDi = new Vector3(0, 0, mSpeed/gravity*4);
                moveDi = transform.TransformDirection(moveDi);
                moveDi *= mSpeed;

                //fallen
                moveDi.y -= gravity * Time.deltaTime;
                controller.Move(moveDi * Time.deltaTime);
            // }
        }
    }

    public void TurnAround() {
        if( gameObject.transform.position.z != zStart ) {
            moveDi = new Vector3(0, 0, mSpeed/gravity*4);
            moveDi = Quaternion.Euler(0, -5 * rotate, 0) * moveDi;
            moveDi = transform.TransformDirection(moveDi);
            moveDi *= mSpeed;

            //fallen
            moveDi.y -= gravity * Time.deltaTime;
            controller.Move(moveDi * Time.deltaTime);
        } else {
            direction = rotate;
        }
    }
}
