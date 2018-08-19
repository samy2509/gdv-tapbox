using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour {
	void OnCollisionEnter( Collision col ){
        if (col.gameObject.transform.tag == "Barrier") {
            TurnAround();
        }
    }

	public void TurnAround() {
        if( (transform.rotation.y != -90f || transform.rotation.y != 90f) ) {
            for(int i = 0; i < 45; i++) {
                transform.Rotate(0, 4, 0);
            }

            //direction = rotate;
        } else {
            //direction = rotate;
        }

        Debug.Log("Entered");
    }
}
