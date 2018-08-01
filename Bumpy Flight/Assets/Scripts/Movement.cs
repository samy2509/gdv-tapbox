using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float mSpeed = 10.0f;
    private float gravity = 14.0f;
    private float jumpForce = 14.0f;
    private Vector3 moveDi = Vector3.zero;

    void Start () {
        mSpeed = 7.0f;
	}

	void Update () {
        
        //fallen
        moveDi.y -= gravity * Time.deltaTime;

    }

}
