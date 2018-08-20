using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raetsel : MonoBehaviour {

	public int[] 		riddle;			//Array wird zufällig gefüllt für TouchRockRiddle
	public GameObject[] rocks;			//Array für Steine-Prefabs
	public List<int> 	orderList;		//Liste für Berührung von Steinen (TouchRockRiddle)

	public int rand;					//Zufallsvariable für zufällige Wahl eines Rätsels
	public int sperre;					//Sperrvariable für RaetlseCollisions
	public int set1;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)
	public int set2;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)
	public int set3;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)

	private int laenge;							//Länge des Zufallsmeshs
	private generiereZufallsmesh meshScript; 	//Script generiereZufallsmesh
	
	void Start() 
	{
		new GameObject("Rocks");
		new GameObject("LittleRocks").transform.SetParent(GameObject.Find("Rocks").transform);

		meshScript = GameObject.Find("Zufallshöhle").GetComponent<generiereZufallsmesh>();
		laenge = meshScript.laenge;

		set1 = 0;
		set2 = 0;
		set3 = 0;
		riddle = new int[3];
		rand = Random.Range(0, 1);
		orderList = new List<int>();

		spawnEntraceExitFire();
		spawnLittleRocks();
		ChooseRiddle();
	}

	private void ChooseRiddle() 
	{
		switch(rand) 
		{
			case 0:
				TouchRockRiddle();
				//TouchBetweenStairs();
				break;
			case 1:
				//TouchRockRiddle();
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

	private void TouchRockRiddle() 
	{
		//Instantiate (rocks[(Random.Range(0, rocks.Length))], 
		//inst.AddComponent<Script>();
		GameObject stairs =	Instantiate (rocks[18],				//Stairs
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.Euler(0, 0, 0)) 
								as GameObject;
		stairs.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject touchstones =	Instantiate (rocks[19],		//TouchStones
									new Vector3 (laenge/3, 0.0f, 0.0f), 
									Quaternion.Euler(0, 0, 0)) 
									as GameObject;
		touchstones.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject wall =	Instantiate (rocks[20],				//Wall
							new Vector3 (laenge/2 + 2, 0.0f, 0.0f), 
							Quaternion.Euler(0, 0, 0)) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);

		GameObject after =	Instantiate (rocks[14],				//rock o
							new Vector3 (laenge/2 + 16.0f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		RandomNumberOrder();
	}

    private void RandomNumberOrder()
    {
        for (int i = 0; i < 3; i++)
        {
            riddle[i] = i;
        }
        
		// Fisher-Yates Shuffle
		for (int i = 0; i < riddle.Length; i++)
        {
            int tmp = riddle[i];
            int index = Random.Range(0, riddle.Length);
            riddle[i] = riddle[index];
            riddle[index] = tmp;
        }
    }

	private void TouchBetweenStairs()
	{
		GameObject stairs =	Instantiate (rocks[21],				//TouchBetweenStairs
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.Euler(0, 0, 0)) 
								as GameObject;
		stairs.transform.SetParent(GameObject.Find("Rocks").transform);
	}

	private void spawnEntraceExitFire () 
	{
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

		GameObject fire =	Instantiate (rocks[22],  			//Fire
							new Vector3 (laenge/2, 6.0f, -0.5f), 
							Quaternion.Euler(-45.0f, 0, 0)) 
							as GameObject;
		fire.transform.SetParent(GameObject.Find("Rocks").transform);
		fire.name = "fire";
	}

	private void spawnLittleRocks () 
	{
		GameObject rock1b = Instantiate (rocks[0],  			//rock a
							new Vector3 (Random.Range (8.0f, 12.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock1b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1b.name = "rock1b";

		GameObject rock2b = Instantiate (rocks[2],  			//rock c
							new Vector3 (Random.Range (17.0f, 20.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock2b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 5.0f), Random.Range (6.5f, 7.0f));
		rock2b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock2b.name = "rock2b";

		GameObject rock3b = Instantiate (rocks[4],  			//rock e
							new Vector3 (Random.Range (25.0f, 30.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock3b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock3b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock3b.name = "rock3b";

		GameObject rock4b = Instantiate (rocks[6],  			//rock g
							new Vector3 (laenge/2 - Random.Range (1.0f, 3.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock4b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock4b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock4b.name = "rock4b";


		GameObject rock5b = Instantiate (rocks[4],  			//rock e
							new Vector3 (laenge/2 + Random.Range (7.0f, 11.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock5b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock5b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock5b.name = "rock5b";

		GameObject rock6b = Instantiate (rocks[2],  			//rock c
							new Vector3 (laenge/2 + Random.Range (15.0f, 18.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock6b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 5.0f), Random.Range (6.5f, 7.0f));
		rock6b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock6b.name = "rock6b";

		GameObject rock7b = Instantiate (rocks[0],  			//rock a
							new Vector3 (laenge - Random.Range (13.0f, 15.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock7b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock7b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock7b.name = "rock7b";

		

		GameObject rock1f = Instantiate (rocks[1],  			//rock b
							new Vector3 (Random.Range (2.0f, 7.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock1f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1f.name = "rock1f";

		GameObject rock2f = Instantiate (rocks[3],  			//rock d
							new Vector3 (Random.Range (12.0f, 16.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock2f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock2f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock2f.name = "rock2f";

		GameObject rock3f = Instantiate (rocks[5],  			//rock f
							new Vector3 (Random.Range (18.0f, 24.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock3f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock3f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock3f.name = "rock3f";

		GameObject rock4f = Instantiate (rocks[7],  			//rock h
							new Vector3 (laenge/2 - Random.Range (1.0f, 3.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock4f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock4f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock4f.name = "rock4f";


		GameObject rock5f = Instantiate (rocks[5],  			//rock f
							new Vector3 (laenge/2 + Random.Range (5.0f, 9.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock5f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock5f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock5f.name = "rock5f";

		GameObject rock6f = Instantiate (rocks[1],  			//rock b
							new Vector3 (laenge/2 + Random.Range (11.0f, 20.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock6f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock6f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock6f.name = "rock6f";

		GameObject rock7f = Instantiate (rocks[3],  			//rock d
							new Vector3 (laenge - Random.Range (8.0f, 12.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock7f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock7f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock7f.name = "rock7f";	
	}
}