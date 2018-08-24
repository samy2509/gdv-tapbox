using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDuck : MonoBehaviour {
 	public float mSpeed                     = 6f;           // Geschwindigkeit, in der sich das Objekt bewegt
    private float gravity                   = 44.0f;        // Anziehungskraft, die auf das Objekt wirkt
    private float jumpForce                 = 24.0f;        // Stärke mit der das Objekt vom Boden abspringt
    private int bounceSpeed                 = -1;
    private Vector3 moveDi                  = Vector3.zero;

    private generateCurve       curve;
    private ObjectRandomSpawn   ors;
    private CharacterController controller;
    private float               lastTurn;
    private UIController		uicontroller;
    private int                 health;                     // Die Leben des Gegners
    private GameObject          pointsText;                 // Text, der beim Tod angezeigt werden soll
    private GameObject          caves;                      // Gruppierungsobjekt der Höhlen im Level

    void Start () {
        controller      = gameObject.GetComponent<CharacterController>();
		curve		    = GameObject.Find("LevelGenerator").GetComponent<generateCurve>();
        ors 		    = GameObject.Find("LevelGenerator").GetComponent<ObjectRandomSpawn>();
        uicontroller    = GameObject.Find("LevelManager").GetComponent<UIController>();
        caves           = GameObject.Find("Caves");
        mSpeed          = 10.0f;
        lastTurn        = 0;
        health          = 100;
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

        foreach (Transform child in caves.transform) {
            if ( gameObject.transform.position.x > child.transform.position.x - 10 && gameObject.transform.position.x < child.transform.position.x + 10 ) {
                if(gameObject.transform.position.x > child.transform.position.x && gameObject.transform.position.x < child.transform.position.x + 10 && gameObject.transform.forward.x < 0) {
                    TurnAround();
                } else if( gameObject.transform.position.x < child.transform.position.x && gameObject.transform.position.x > child.transform.position.x - 10 && gameObject.transform.forward.x > 0 ) {
                    TurnAround();
                }
            }
        }

        if( gameObject != null && gameObject.transform.position.x + 5f < curve.turtle.transform.position.x ) {
			moveDi.y -= gravity * Time.deltaTime;
            controller.Move(moveDi * Time.deltaTime);
        }

        // Rotation an Boden anpassen
        Ray ray = new Ray(transform.position, -(transform.up));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Floor"), QueryTriggerInteraction.Ignore)) {  
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if(bounceSpeed < 0) {
                transform.Rotate(0, -90, 0);
            } else {
                transform.Rotate(0, 90, 0);
            }
        }
    }

    void OnTriggerEnter( Collider col ){
        if (col.gameObject.tag == "Barrier" && gameObject.tag != "Boss") {
            TurnAround();
        }

        if (col.gameObject.tag == "Egg" && gameObject.tag != "Boss") {
            health = 0;
            DestroyEnemy();
        } else if (col.gameObject.tag == "Egg") {
            health -= 25;
            DestroyEnemy();
        }
    }

    // Despawnt den Gegner und Spawnt den Punktetext
    public void DestroyEnemy() {
        if( health == 0 ) {
            pointsText      = Instantiate(Resources.Load("Punkte-Text"),
            gameObject.transform.position,
            Quaternion.identity) as GameObject;
            pointsText.AddComponent<UIPoints>();

            uicontroller.AddToScore(30);
            pointsText.GetComponent<UIPoints>().Points(30, gameObject.transform.position);

            ors.DeleteEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    // Lässt den Gegner eine 180-Grad-Wende durchführen
    public void TurnAround() {
        if( (transform.rotation.y != -90f || transform.rotation.y != 90f) ) {
            transform.Rotate(0, 180, 0);

            lastTurn = transform.position.x;
            bounceSpeed = -bounceSpeed;
        }
    }
}
