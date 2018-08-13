using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raetsel : MonoBehaviour {

	public GameObject[] rocks;
	private generiereZufallsmesh meshL;
	private int laenge;

	void Start() {
		new GameObject("Rocks");
		new GameObject("LittleRocks").transform.SetParent(GameObject.Find("Rocks").transform);

		meshL = GameObject.Find("Zufallshöhle").GetComponent<generiereZufallsmesh>();
		laenge = meshL.laenge;

		spawnEntraceExit();
		spawnLittleRocks();
		ChooseRiddle();
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
		//gegnerInst.AddComponent<Movement>();
		GameObject stairs =	Instantiate (rocks[18],				//Stairs
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.Euler(0, 0, 0)) 
								as GameObject;
		stairs.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject touchstones =	Instantiate (rocks[19],				//TouchStones
									new Vector3 (laenge/3, 0.0f, 0.0f), 
									Quaternion.Euler(0, 0, 0)) 
									as GameObject;
		touchstones.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject wall =	Instantiate (rocks[20],				//Wall
							new Vector3 (laenge - 30, 0.0f, 0.0f), 
							Quaternion.Euler(0, 0, 0)) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);
	}


	private void spawnLittleRocks () {
		GameObject rock1b = Instantiate (rocks[0],  			//rock a
							new Vector3 (Random.Range (10.0f, 15.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock1b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1b.name = "rock1b";

		GameObject rock1f = Instantiate (rocks[1],  			//rock b
							new Vector3 (Random.Range (5.0f, 10.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock1f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1f.name = "rock1f";
	}


	private void spawnEntraceExit () {
		GameObject entrace =	Instantiate (rocks[16],  		//Höhl_h
								new Vector3 (2.6f, 1.71f, -16.97f), 
								Quaternion.Euler(0, 0, 0)) 
								as GameObject;
		entrace.transform.SetParent(GameObject.Find("Rocks").transform);
		entrace.name = "entrance";

		GameObject exit =	Instantiate (rocks[17],  			//Höhl_h2
							new Vector3 (laenge - 15.0f, 1.7f, -17.8f), 
							Quaternion.Euler(0, -14.8f, 0)) 
							as GameObject;
		exit.transform.SetParent(GameObject.Find("Rocks").transform);
		exit.name = "exit";
	}
}

