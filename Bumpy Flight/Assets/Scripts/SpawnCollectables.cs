using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour {

	public GameObject[] collectables;
	//private generateCurve curve;
	//private Vector3 pos;

	void Start() 
	{
		//curve	= GetComponent<generateCurve>();
		InvokeRepeating("spawn", 10.0f, 10.0f);
	}

    public void spawn()
    {
		//pos = curve.posGetter;
        GameObject collect = 	Instantiate(collectables[0],
                            	new Vector3(Camera.main.gameObject.transform.position.x + 25.0f, Camera.main.gameObject.transform.position.y - 7.5f, 6.4f),
								Quaternion.Euler(0, 0, 0))
								as GameObject;

        // BoxCollider bc = collect.AddComponent<BoxCollider>();
        // bc.isTrigger = true;
        // CharacterController cc = collect.AddComponent<CharacterController>();

        // bc.size = new Vector3(2f, 2.3f, 4f);
        // bc.center = new Vector3(0f, 1.15f, 0f);

        // cc.radius = 1.35f;
        // cc.center = new Vector3(0f, 1.35f, 0f);
    }

}