using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDuck : MonoBehaviour {
 	public float mSpeed                     = 8f;           // Geschwindigkeit, in der sich das Objekt bewegt
    private float gravity                   = 44.0f;        // Anziehungskraft, die auf das Objekt wirkt
    private float jumpForce                 = 24.0f;        // Stärke mit der das Objekt vom Boden abspringt
    private Vector3 moveDi                  = Vector3.zero;

    private generateCurve curve;
    private CharacterController controller;
    private float               lastTurn;

    void Start () {
        controller  = gameObject.GetComponent<CharacterController>();
		curve		= GameObject.Find("LevelGenerator").GetComponent<generateCurve>();
        mSpeed      = 10.0f;
        lastTurn    = 0;
	}

	void Update () {
		if( controller.isGrounded && gameObject != null && gameObject.transform.position.x + 5f < curve.turtle.transform.position.x) {
			moveDi = new Vector3(0, 0, mSpeed/gravity*4);
            moveDi = transform.TransformDirection(moveDi);
			moveDi *= mSpeed;
		}

        if(Random.Range(0, 100) == 5 && (transform.position.x > lastTurn + 20f || transform.position.x < lastTurn - 20f)) {
            TurnAround();
        } else if ( Random.Range(0, 100) == 5 && controller.isGrounded ) {
			moveDi.y = jumpForce;
		}

        if( gameObject != null && gameObject.transform.position.x + 5f < curve.turtle.transform.position.x ) {
			moveDi.y -= gravity * Time.deltaTime;
            controller.Move(moveDi * Time.deltaTime);
        }
    }

    void OnCollisionEnter( Collision col ){
        if (col.gameObject.tag == "Barrier") {
            TurnAround();
        }
    }

    public void TurnAround() {
        if( (transform.rotation.y != -90f || transform.rotation.y != 90f) ) {
            for(int i = 0; i < 45; i++) {
                transform.Rotate(0, 4, 0);
            }

            lastTurn = transform.position.x;
        }
    }
}
