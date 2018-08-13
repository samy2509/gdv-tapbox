using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaetselCollisions : MonoBehaviour {

	public GameObject s1;
	public GameObject s2;

	void Start() {
		s1 = GameObject.Find("Säule1");
		s2 = GameObject.Find("Säule2");

	}

	void OnTriggerEnter( Collider col ){
        if (col.gameObject.tag == "player2" && gameObject.tag == "rock3") {
            doIt();
        }
    }

	void doIt() {
		Vector3 newpos1 = s1.transform.position;
		newpos1.y -= 5.0f;
		s1.transform.position = newpos1;

		Vector3 newpos2 = s2.transform.position;
		newpos2.y -= 5.0f;
		s2.transform.position = newpos2;
	}
}
