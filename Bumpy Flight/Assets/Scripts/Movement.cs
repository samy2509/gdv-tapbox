using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float mSpeed                     = 10f;           // Geschwindigkeit, in der sich das Objekt bewegt
    private float gravity                   = 44.0f;        // Anziehungskraft, die auf das Objekt wirkt
    private float bounceSpeed               = -.1f;         // Geschwindigkeit zum Hüpfen
    private Vector3 moveDi                  = Vector3.zero;

    private generateCurve curve;
    private CharacterController controller;
    private float               lastTurn;

    void Start () {
        controller  = gameObject.GetComponent<CharacterController>();
        curve		= GameObject.Find("LevelGenerator").GetComponent<generateCurve>();
        mSpeed      = 6.0f;
        lastTurn    = 0;
	}

	void Update () {
        if(Random.Range(0, 100) == 5 && (transform.position.x > lastTurn + 20f || transform.position.x < lastTurn - 20f)) {
            TurnAround();
        }

        if( gameObject != null && gameObject.transform.position.x + 5f < curve.turtle.transform.position.x ) {
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

    void OnTriggerEnter( Collider col ){
        if (col.gameObject.tag == "Barrier" && gameObject.tag != "Boss") {
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

        bounceSpeed = -bounceSpeed;
    }
}
