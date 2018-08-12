using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raetsel : MonoBehaviour {

	public GameObject[] rocks;

	void Start() {
		new GameObject("Rocks");
		ChooseRiddle();
	}
	
	void Update () {
		
	}

	private void ChooseRiddle() {
		int rand = Random.Range(0, 1);

		switch(rand) {
			case 0:
				PushRockRiddle();
				break;
			case 1:

				break;
			case 2:

				break;
			case 3:

				break;
			case 4:
				//nothing
				break;
		}
	}

	private void PushRockRiddle() {
		//Instantiate (rocks[(Random.Range(0, rocks.Length))], 
		GameObject rock1 = 	Instantiate (rocks[0],  
							new Vector3 (Random.Range (10.0f, 15.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock1.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject rock2 = Instantiate (rocks[1],  
							new Vector3 (Random.Range (5.0f, 10.0f), 8.0f, -3.5f), 
							Quaternion.Euler(180, Random.Range (0, 360), 0)) 
							as GameObject;
		rock2.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock2.transform.SetParent(GameObject.Find("Rocks").transform);

		//gegnerInst.AddComponent<Movement>();

		
	}
}
