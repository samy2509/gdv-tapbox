using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float mSpeed                     = 10f;           // Geschwindigkeit, in der sich das Objekt bewegt
    private float gravity                   = 44.0f;        // Anziehungskraft, die auf das Objekt wirkt
    private float jumpForce                 = 14.0f;        // Stärke mit der das Objekt vom Boden abspringt
    private float bounceSpeed               = -.1f;         // Geschwindigkeit zum Hüpfen
    private Vector3 moveDi                  = Vector3.zero;

    private CharacterController controller;
    private Vector3             zStart;
    private float               lastTurn;
    private int                 rotate;

    void Start () {
        controller  = gameObject.GetComponent<CharacterController>();
        mSpeed      = 7.0f;
        zStart      = gameObject.transform.position;
        lastTurn    = 0;
	}

	void Update () {
        if(Random.Range(0, 100) == 5 && (transform.position.x > lastTurn + 20f || transform.position.x < lastTurn - 20f)) {
            TurnAround();
        }

        if( gameObject != null ) {
            moveDi = new Vector3(0, 0, mSpeed/gravity*4);
            moveDi = transform.TransformDirection(moveDi);
            moveDi *= mSpeed;

            if(gameObject.name != "Sheep(Clone)") {
                moveDi.y -= gravity * Time.deltaTime;
                controller.Move(moveDi * Time.deltaTime);
            } else {
                transform.Translate(0f, 0f, bounceSpeed);
            }
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

        bounceSpeed = bounceSpeed * -1f;
    }
}
