using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raetsel : MonoBehaviour 
{
	public int[] 		riddle;			//Array wird zufällig gefüllt für TouchRockRiddle
	public GameObject[] rocks;			//Array für Steine-Prefabs
	public GameObject[] other;			//Array für weitere Prefabs (Fackel, Powerups)
	public List<int> 	orderList;		//Liste für Berührung von Steinen (TouchRockRiddle)

	public int rand;					//Zufallsvariable für zufällige Wahl eines Rätsels - Nummer des Rätsels zur Identifikation
	public int sperre;					//Sperrvariable für RaetlseCollisions
	public int set1;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)
	public int set2;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)
	public int set3;					//Sperrvariable für RaetlseCollisions (TouchRockRiddle)
	public int isHitting;				//Sperrvariable für RaetlseCollisions (FallingStones)

	private float pos;							//(y)-Position der beweglichen Platten
	private int laenge;							//Länge des Zufallsmeshs
	private LevelManager levelManagerScript;	//Script LevelManager
	private generiereZufallsmesh meshScript; 	//Script generiereZufallsmesh
	
	void Start() 
	{
		new GameObject("Rocks");
		new GameObject("LittleRocks").transform.SetParent(GameObject.Find("Rocks").transform);
		new GameObject("Other");

		meshScript 			= GameObject.Find("Zufallshöhle").GetComponent<generiereZufallsmesh>();
		levelManagerScript	= GameObject.Find("LevelManager").GetComponent<LevelManager>();

		laenge 	= meshScript.laenge;
        rand 	= Random.Range(0, 5);
		
		if (rand == 0)
        {
            set1 = 0;
            set2 = 0;
            set3 = 0;

            riddle 		= new int[3];
            orderList 	= new List<int>();
        }

		spawnEntraceExitTorch();
		spawnLittleRocks();
		ChooseRiddle();

		if (rand == 1)
        {
            pos = GameObject.Find("Flagstone1").transform.position.y - 0.5f;
        }

		else if (rand == 3) 
		{
			isHitting = 0;
		}
	}

	void Update () 
	{
        if (rand == 1)
        {
            // !!! Math Ping Pong Code !!!
            GameObject.Find("Flagstone1").transform.position = new Vector3(GameObject.Find("Flagstone1").transform.position.x,
                                                                            Mathf.PingPong(Time.time * 2.7f, 3.0f) + pos,
                                                                            GameObject.Find("Flagstone1").transform.position.z);
            GameObject.Find("Flagstone2").transform.position = new Vector3(GameObject.Find("Flagstone2").transform.position.x,
                                                                            Mathf.PingPong(Time.time * 2.0f, 3.0f) + pos,
                                                                            GameObject.Find("Flagstone2").transform.position.z);
        }
		else if (rand == 4) 
		{
			if (GameObject.Find("UpDownRock1").transform.position.y <= -9.0f)
			{
				StartCoroutine(WaitAndTransform("UpDownRock1"));
			} 
			else if (GameObject.Find("UpDownRock2").transform.position.y <= -9.0f)
			{
				StartCoroutine(WaitAndTransform("UpDownRock2"));
			} 
			else if (GameObject.Find("UpDownRock3").transform.position.y <= -9.0f)
			{
				StartCoroutine(WaitAndTransform("UpDownRock3"));
			} 
			else if (GameObject.Find("UpDownRock4").transform.position.y <= -9.0f)
			{
				StartCoroutine(WaitAndTransform("UpDownRock4"));
			} 
			else if (GameObject.Find("UpDownRock5").transform.position.y <= -9.0f)
			{
				StartCoroutine(WaitAndTransform("UpDownRock5"));
			} 
		}
	}

	private void ChooseRiddle() 
	{
		switch(rand) 
		{
			case 0:
				TouchRockRiddle();
				break;
			case 1:
				FireInTheCave();
				break;
			case 2:
				BetterRunFast();
				break;
			case 3:
				FallingStones();
				break;
			case 4:
				UpDownRock();
				break;
		}
	}

	private void TouchRockRiddle() 
	{
		GameObject stairs =	Instantiate (rocks[18],				//Stairs
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.identity) 
								as GameObject;
		stairs.transform.SetParent(GameObject.Find("Rocks").transform);
		stairs.name = "Stairs";

		GameObject touchstones =	Instantiate (rocks[19],		//TouchStones
									new Vector3 (laenge/3, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		touchstones.transform.SetParent(GameObject.Find("Rocks").transform);
		touchstones.name = "TouchStones";

		GameObject wall =	Instantiate (rocks[20],				//Wall
							new Vector3 (laenge/2 + 2, 0.0f, 0.0f), 
							Quaternion.identity) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);
		wall.name = "Wall";

		GameObject after =	Instantiate (rocks[14],				//rock o
							new Vector3 (laenge/2 + 16.0f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		RandomNumberOrder();

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
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

	private void FireInTheCave() 
	{
		GameObject firestairs =	Instantiate (rocks[22],			//FireBetweenStairs
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.identity) 
								as GameObject;
		firestairs.transform.SetParent(GameObject.Find("Rocks").transform);
		firestairs.name = "FireBetweenStairs";

		GameObject oneUp =	Instantiate (other[1],				//PowerUpContainerRed
							new Vector3 (laenge/6, Random.Range(1.0f, 2.0f), -10.8f), 
							Quaternion.identity) 
							as GameObject;
		oneUp.transform.SetParent(GameObject.Find("Other").transform);
		oneUp.tag = "leben";

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	private void BetterRunFast () 
	{
		GameObject flagstonestairs =	Instantiate (rocks[23],			//FlagstonesBetweenStairs
										new Vector3 (laenge/5, 0.0f, 0.0f), 
										Quaternion.identity) 
										as GameObject;
		flagstonestairs.transform.SetParent(GameObject.Find("Rocks").transform);
		flagstonestairs.name = "FlagstonesBetweenStairs";

		GameObject flagstones =	Instantiate (rocks[24],			//Flagstones
										new Vector3 (laenge/5, 0.0f, 0.0f), 
										Quaternion.identity) 
										as GameObject;
		flagstones.transform.SetParent(GameObject.Find("Rocks").transform);
		flagstones.name = "flagstones";

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	public void spawnStonesAgain () 
	{
		if (rand == 2)
        {
            Destroy(GameObject.Find("flagstones"));

            GameObject flagstones = Instantiate(rocks[24],          //Flagstones
                                    new Vector3(laenge / 5, 0.0f, 0.0f),
                                    Quaternion.identity)
                                    as GameObject;
            flagstones.transform.SetParent(GameObject.Find("Rocks").transform);
            flagstones.name = "flagstones";
        }
        else if (rand == 3)
        {
            Destroy(GameObject.Find("fallingstones"));

            GameObject fallingStones = 	Instantiate(rocks[26],               //FallingStones
                                    	new Vector3(laenge / 8 + 10.0f, 14.0f, 0.0f),
                                    	Quaternion.identity)
                                    	as GameObject;
            fallingStones.transform.SetParent(GameObject.Find("Rocks").transform);
            fallingStones.name = "fallingstones";

			GameObject.Find("GreenLamp1").GetComponent<MeshRenderer>().enabled = false;
        	GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = true;

			Destroy(GameObject.Find("Wall"));

            GameObject wall = 	Instantiate(rocks[20],                //Wall
                            	new Vector3(laenge / 2 - 3.0f, 0.0f, 0.0f),
                            	Quaternion.identity)
                            	as GameObject;
            wall.transform.SetParent(GameObject.Find("Rocks").transform);
            wall.name = "Wall";
        }
    }

	private void FallingStones () 
	{
		GameObject trafficRock =	Instantiate (rocks[25],				//TrafficRock
									new Vector3 (laenge/8, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		trafficRock.transform.SetParent(GameObject.Find("Rocks").transform);
		trafficRock.name = "TrafficRock";

		GameObject fallingStones =	Instantiate (rocks[26],				//FallingStones
									new Vector3 (laenge/8 + 10.0f, 14.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		fallingStones.transform.SetParent(GameObject.Find("Rocks").transform);
		fallingStones.name = "fallingstones";

		GameObject wall =	Instantiate (rocks[20],				//Wall
							new Vector3 (laenge/2 - 3.0f, 0.0f, 0.0f), 
							Quaternion.identity) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);
		wall.name = "Wall";

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	private void UpDownRock()
	{
		GameObject upDownRocks =	Instantiate (rocks[27],				//UpDownRocks
									new Vector3 (laenge/8, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		upDownRocks.transform.SetParent(GameObject.Find("Rocks").transform);
		upDownRocks.name = "UpDownRocks";

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	IEnumerator WaitAndTransform(string name)
    {
        GameObject.Find(name).GetComponent<Rigidbody>().isKinematic = true;

        Vector3 newpos = GameObject.Find(name).transform.position;
        for (int i = 0; i < 10; i++)
        {
            newpos.y += 0.9f;
            GameObject.Find(name).transform.position = newpos;
            yield return new WaitForSeconds(0.01f);
        }

        GameObject.Find(name).GetComponent<Rigidbody>().isKinematic = false;
    }

    private void spawnEntraceExitTorch () 
	{
		GameObject entrace =	Instantiate (rocks[16],  		//Höhl_h
								new Vector3 (2.6f, 1.71f, -16.97f), 
								Quaternion.identity) 
								as GameObject;
		entrace.transform.SetParent(GameObject.Find("Rocks").transform);
		entrace.name = "entrance";

		GameObject exit =	Instantiate (rocks[17],  			//Höhl_h2
							new Vector3 (laenge - 15.0f, 1.7f, -17.8f), 
							Quaternion.Euler(0, -14.8f, 0)) 
							as GameObject;
		exit.transform.SetParent(GameObject.Find("Rocks").transform);
		exit.name = "exit";

		GameObject torch =	Instantiate (other[0],  			//Fackel
							new Vector3 (laenge/2, 6.0f, -0.5f), 
							Quaternion.Euler(-45.0f, 0, 0)) 
							as GameObject;
		torch.transform.SetParent(GameObject.Find("Other").transform);
		torch.name = "torch";
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