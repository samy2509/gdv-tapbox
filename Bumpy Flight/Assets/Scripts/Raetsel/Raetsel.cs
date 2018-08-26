/*
 * Raetsel.cs
 *
 * Script für die zufäliige Generierung von Rätseln, Hindernissen und Environment im Nebenlevel "Höhle".
 * Das Script ist LevelManager in Nebenlevel zugewiesen.
 *
 * Funktionen:
 *  	ChooseRiddle()		TouchRockRiddle()		RandomNumberOrder()		FireInTheCave()	
 *  	BetterRunFast()		spawnStonesAgain()		FallingStones()			UpDownRock()
 *  	IEnumerator WaitAndTransform(string name)	spawnLittleRocks()		spawnEntraceExitTorch()
 *  	
 * Quellen:
 *		Für die selbst erstellten Prefabs wurden teils Modelle aus folgenden Assets verwendet:
 *			"Low poly styled rocks" von DANIEL ROBNIK https://assetstore.unity.com/packages/3d/props/exterior/low-poly-styled-rocks-43486
 *			"PowerUp particles" von MHLAB https://assetstore.unity.com/packages/vfx/particles/powerup-particles-16458
 *			"Free PBR Lamps" von NEW SOLUTION STUDIO https://assetstore.unity.com/packages/3d/props/interior/free-pbr-lamps-70181
 * Stand 25.08.2018
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raetsel : MonoBehaviour 
{
	public int[] 		riddle;			// Array wird zufällig gefüllt für TouchRockRiddle
	public GameObject[] rocks;			// Array für Steine-Prefabs
	public GameObject[] other;			// Array für weitere Prefabs (Fackel, Powerups)
	public List<int> 	orderList;		// Liste für Berührung von Steinen (TouchRockRiddle)

	public int rand;					// Zufallsvariable für zufällige Wahl eines Rätsels - Nummer des Rätsels zur Identifikation
	public int sperre;					// Sperrvariable für RaetselCollisions
	public int set1;					// Sperrvariable für RaetselCollisions (TouchRockRiddle)
	public int set2;					// Sperrvariable für RaetselCollisions (TouchRockRiddle)
	public int set3;					// Sperrvariable für RaetselCollisions (TouchRockRiddle)
	public int isHitting;				// Sperrvariable für RaetselCollisions (FallingStones)

	private float pos;							// (y)-Position der beweglichen Platten
	private int laenge;							// Länge des Zufallsmeshs
	private LevelManager levelManagerScript;	// Script LevelManager
	private generiereZufallsmesh meshScript; 	// Script generiereZufallsmesh
	
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
			// Gehört zu Rätsel / Hindernis 1 FireInTheCave: Steinplatten hoch und runter bewegen
            GameObject.Find("Flagstone1").transform.position = new Vector3(GameObject.Find("Flagstone1").transform.position.x,
                                                                            Mathf.PingPong(Time.time * 2.7f, 3.0f) + pos,
                                                                            GameObject.Find("Flagstone1").transform.position.z);
            GameObject.Find("Flagstone2").transform.position = new Vector3(GameObject.Find("Flagstone2").transform.position.x,
                                                                            Mathf.PingPong(Time.time * 2.0f, 3.0f) + pos,
                                                                            GameObject.Find("Flagstone2").transform.position.z);
        }
		else if (rand == 4) 
		{
			// Gehört zu Rätsel / Hindernis 4 UpDownRock: Steinspitzen wieder hochschnellen lassen via Coroutine WaitAndTransform
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

	//Wählt ein zufälliges Rätsel/Hindernis entsprechend der Zufallszahl rand
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

	/*
	 *	Rätsel 0 TouchRockRiddle
	 *  
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs die für das Rätsel relevanten Prefabs.
	 *	Position, Rotation und Scale der Prefabs werden (teils zufällig) angepasst.
	 *	Das eigentliche Rätsel ist in RaetselCollisions.cs umgesetzt.
 	 *	
	 *	Zugehörige Funktionen: 
	 *		RandomNumberOrder()
	 */
	private void TouchRockRiddle() 
	{
		// Prefab Stairs
		GameObject stairs =	Instantiate (rocks[18],				
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.identity) 
								as GameObject;
		stairs.transform.SetParent(GameObject.Find("Rocks").transform);
		stairs.name = "Stairs";

		// Prefab TouchStones
		GameObject touchstones =	Instantiate (rocks[19],		
									new Vector3 (laenge/3, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		touchstones.transform.SetParent(GameObject.Find("Rocks").transform);
		touchstones.name = "TouchStones";

		// Prefab Wall
		GameObject wall =	Instantiate (rocks[20],				
							new Vector3 (laenge/2 + 2, 0.0f, 0.0f), 
							Quaternion.identity) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);
		wall.name = "Wall";

		// Prefab rock o
		GameObject after =	Instantiate (rocks[14],				
							new Vector3 (laenge/2 + 16.0f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		RandomNumberOrder();

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	/*
	 *	Gehört zu Rätsel 0 TouchRockRiddle
	 *	
	 *	Füllt Array mit den Zahlen 0, 1 und 2 und shuffelt das Array.
 	 *	
	 *	Quelle: 
	 *		Fisher-Yates Shuffle basiert auf https://medium.com/@thelextimes/fisher-yates-algorithm-the-logic-behind-shuffling-98deb8bac210 
	 */
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

	/*
	 *	Rätsel / Hindernis 1 FireInTheCave
	 *  
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs die für das Rätsel / Hindernis relevanten Prefabs.
	 *  Position, Rotation und Scale der Prefabs werden (teils zufällig) angepasst.
	 *	Das eigentliche Rätsel / Hindernis ist in RaetselCollisions.cs umgesetzt.
	 *	
	 *	Zugehörige Funktionen: 
	 *		Update()
	 */
	private void FireInTheCave() 
	{
		// Prefab FireBetweenStairs
		GameObject firestairs =	Instantiate (rocks[22],			
								new Vector3 (laenge/5, 0.0f, 0.0f), 
								Quaternion.identity) 
								as GameObject;
		firestairs.transform.SetParent(GameObject.Find("Rocks").transform);
		firestairs.name = "FireBetweenStairs";

		// Prefab PowerUpContainerRed
		GameObject oneUp =	Instantiate (other[1],				
							new Vector3 (laenge/6, Random.Range(1.0f, 2.0f), -10.8f), 
							Quaternion.identity) 
							as GameObject;
		oneUp.transform.SetParent(GameObject.Find("Other").transform);
		oneUp.tag = "leben";

		// Prefab rock o
		GameObject after =	Instantiate (rocks[14],				
							new Vector3 (laenge/2 + 17.5f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	/*
	 *	Rätsel / Hindernis 2 BetterRunFast
	 *  
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs die für das Rätsel / Hindernis relevanten Prefabs.
	 *  Position, Rotation und Scale der Prefabs werden (teils zufällig) angepasst.
	 *	Das eigentliche Rätsel / Hindernis ist in RaetselCollisions.cs umgesetzt.
	 *	
	 *	Zugehörige Funktionen: 
	 *		spawnStonesAgain()
	 */
	private void BetterRunFast () 
	{
		// Prefab FlagstonesBetweenStairs
		GameObject flagstonestairs =	Instantiate (rocks[23],			
										new Vector3 (laenge/5, 0.0f, 0.0f), 
										Quaternion.identity) 
										as GameObject;
		flagstonestairs.transform.SetParent(GameObject.Find("Rocks").transform);
		flagstonestairs.name = "FlagstonesBetweenStairs";

		// Prefab Flagstones
		GameObject flagstones =	Instantiate (rocks[24],			
										new Vector3 (laenge/5, 0.0f, 0.0f), 
										Quaternion.identity) 
										as GameObject;
		flagstones.transform.SetParent(GameObject.Find("Rocks").transform);
		flagstones.name = "flagstones";

		// Prefab rock o
		GameObject after =	Instantiate (rocks[14],				
							new Vector3 (laenge/2 + 17.5f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	/*
	 *	Gehört zu Rätsel / Hindernis 2 BetterRunFast und 3 FallingStones
	 *  
	 *	Rätsel / Hindernisse werden zurückgesetzt, wenn Player respawnt wird.
	 *  Zerstört die relevanten Preafabs und spawnt sie erneut. Setzt Prefabs sie zurück.
	 *
	 *	Funktion wird bei Respawn extern aufgerufen.
	 */
	public void spawnStonesAgain () 
	{
		// Rätsel / Hindernis 2 BetterRunFast
		if (rand == 2)
        {
            Destroy(GameObject.Find("flagstones"));

			// Prefab Flagstones
            GameObject flagstones = Instantiate(rocks[24],          
                                    new Vector3(laenge / 5, 0.0f, 0.0f),
                                    Quaternion.identity)
                                    as GameObject;
            flagstones.transform.SetParent(GameObject.Find("Rocks").transform);
            flagstones.name = "flagstones";
        }

		// Rätsel / Hindernis 3 FallingStones
        else if (rand == 3)
        {
            Destroy(GameObject.Find("fallingstones"));

			// Prefab FallingStones
            GameObject fallingStones = 	Instantiate(rocks[26],               
                                    	new Vector3(laenge / 8 + 10.0f, 14.0f, 0.0f),
                                    	Quaternion.identity)
                                    	as GameObject;
            fallingStones.transform.SetParent(GameObject.Find("Rocks").transform);
            fallingStones.name = "fallingstones";

			GameObject.Find("GreenLamp1").GetComponent<MeshRenderer>().enabled = false;
        	GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = true;

			Destroy(GameObject.Find("Wall"));

			//Prefab Wall
            GameObject wall = 	Instantiate(rocks[20],                
                            	new Vector3(laenge / 2 - 3.0f, 0.0f, 0.0f),
                            	Quaternion.identity)
                            	as GameObject;
            wall.transform.SetParent(GameObject.Find("Rocks").transform);
            wall.name = "Wall";
        }
    }

	/*
	 *	Rätsel / Hindernis 3 FallingStones
	 *  
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs die für das Rätsel / Hindernis relevanten Prefabs.
	 *  Position, Rotation und Scale der Prefabs werden (teils zufällig) angepasst.
	 *	Das eigentliche Rätsel / Hindernis ist in RaetselCollisions.cs umgesetzt.
	 *	
	 *	Zugehörige Funktionen: 
	 *		spawnStonesAgain()
	 */
	private void FallingStones () 
	{
		// Prefab TrafficRock
		GameObject trafficRock =	Instantiate (rocks[25],				
									new Vector3 (laenge/8, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		trafficRock.transform.SetParent(GameObject.Find("Rocks").transform);
		trafficRock.name = "TrafficRock";

		// Prefab FallingStones
		GameObject fallingStones =	Instantiate (rocks[26],				
									new Vector3 (laenge/8 + 10.0f, 14.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		fallingStones.transform.SetParent(GameObject.Find("Rocks").transform);
		fallingStones.name = "fallingstones";

		// Prefab Wall
		GameObject wall =	Instantiate (rocks[20],				
							new Vector3 (laenge/2 - 3.0f, 0.0f, 0.0f), 
							Quaternion.identity) 
							as GameObject;
		wall.transform.SetParent(GameObject.Find("Rocks").transform);
		wall.name = "Wall";

		// Prefab StonesBetweenStairs
		GameObject stoneStairs =	Instantiate (rocks[28],				
									new Vector3 (laenge/2, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		stoneStairs.transform.SetParent(GameObject.Find("Rocks").transform);
		stoneStairs.name = "StonesBetweenStairs";

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");
	}

	/*
	 *	Rätsel / Hindernis 4 UpDownRock
	 *  
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs die für das Rätsel / Hindernis relevanten Prefabs.
	 *  Position, Rotation und Scale der Prefabs werden (teils zufällig) angepasst.
	 *	Das eigentliche Rätsel / Hindernis ist in RaetselCollisions.cs umgesetzt.
	 *
	 *	Zugehörige Funktionen: 
	 *		WaitAndTransform(string name)
	 *		Update()
	 */
	private void UpDownRock()
	{
		// Prefab UpDownRocks
		GameObject upDownRocks =	Instantiate (rocks[27],				
									new Vector3 (laenge/6, 0.0f, 0.0f), 
									Quaternion.identity) 
									as GameObject;
		upDownRocks.transform.SetParent(GameObject.Find("Rocks").transform);
		upDownRocks.name = "UpDownRocks";

		// Prefab rock o
		GameObject after =	Instantiate (rocks[14],				
							new Vector3 (laenge/2 + 16.0f, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		after.transform.SetParent(GameObject.Find("Rocks").transform);
		after.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		// Prefab rock o
		GameObject before =	Instantiate (rocks[14],				
							new Vector3 (laenge/7, 0.0f, -6.7f), 
							Quaternion.Euler(-90, 0, 0)) 
							as GameObject;
		before.transform.SetParent(GameObject.Find("Rocks").transform);
		before.transform.localScale = new Vector3 (Random.Range(3.1f, 4.0f), 2.22f, 1.0f);

		levelManagerScript.currentCheckpoint = GameObject.Find("Spawner");        
	}

	/*
	 *	Gehört zu Rätsel / Hindernis 4 UpDownRock
	 *	
	 *  Coroutine lässt Steinspitzen wieder hochschnellen.
	 *	Wird in Update() gestartet.
	 */
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

	/*
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs den Höhleneingang und -ausgang sowie die Fackel.
	 *	Position und Rotation der Prefabs werden angepasst.
	 */
    private void spawnEntraceExitTorch () 
	{
		// Prefab Höhl_h
		GameObject entrace =	Instantiate (rocks[16],  		
								new Vector3 (2.6f, 1.71f, -16.97f), 
								Quaternion.identity) 
								as GameObject;
		entrace.transform.SetParent(GameObject.Find("Rocks").transform);
		entrace.name = "entrance";

		// Prefab Höhl_h2
		GameObject exit =	Instantiate (rocks[17],  			
							new Vector3 (laenge - 15.0f, 1.7f, -17.8f), 
							Quaternion.Euler(0, -14.8f, 0)) 
							as GameObject;
		exit.transform.SetParent(GameObject.Find("Rocks").transform);
		exit.name = "exit";

		// Prefab Fackel
		GameObject torch =	Instantiate (other[0],  			
							new Vector3 (laenge/2, 6.0f, -0.5f), 
							Quaternion.Euler(-45.0f, 0, 0)) 
							as GameObject;
		torch.transform.SetParent(GameObject.Find("Other").transform);
		torch.name = "torch";
	}

	/*
	 *	Spawnt in Abhängigkeit der Länge des Zufallsmeshs Steine im Vorder- und Hintergrund.
	 *  Position, Rotation und Scale der Prefabs werden zufällig angepasst.
	 */
	private void spawnLittleRocks () 
	{
		// Prefab rock a
		GameObject rock1b = Instantiate (rocks[0],  			
							new Vector3 (Random.Range (8.0f, 12.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock1b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1b.name = "rock1b";

		// Prefab rock c
		GameObject rock2b = Instantiate (rocks[2],  			
							new Vector3 (Random.Range (17.0f, 20.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock2b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 5.0f), Random.Range (6.5f, 7.0f));
		rock2b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock2b.name = "rock2b";

		// Prefab rock e
		GameObject rock3b = Instantiate (rocks[4],  			
							new Vector3 (Random.Range (25.0f, 30.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock3b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock3b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock3b.name = "rock3b";

		// Prefab rock g
		GameObject rock4b = Instantiate (rocks[6],  			
							new Vector3 (laenge/2 - Random.Range (1.0f, 3.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock4b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock4b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock4b.name = "rock4b";


		// Prefab rock e
		GameObject rock5b = Instantiate (rocks[4],  			
							new Vector3 (laenge/2 + Random.Range (7.0f, 11.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock5b.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock5b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock5b.name = "rock5b";

		// Prefab rock c
		GameObject rock6b = Instantiate (rocks[2],  			
							new Vector3 (laenge/2 + Random.Range (15.0f, 18.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock6b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 5.0f), Random.Range (6.5f, 7.0f));
		rock6b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock6b.name = "rock6b";

		// Prefab rock a
		GameObject rock7b = Instantiate (rocks[0],  			
							new Vector3 (laenge - Random.Range (13.0f, 15.0f), 0.0f, -3.5f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock7b.transform.localScale = new Vector3 (Random.Range (5.0f, 10.0f), Random.Range (1.0f, 3.0f), Random.Range (6.5f, 7.0f));
		rock7b.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock7b.name = "rock7b";

		

		// Prefab rock b
		GameObject rock1f = Instantiate (rocks[1],  			
							new Vector3 (Random.Range (2.0f, 7.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock1f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock1f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock1f.name = "rock1f";

		// Prefab rock d
		GameObject rock2f = Instantiate (rocks[3],  			
							new Vector3 (Random.Range (12.0f, 16.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock2f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock2f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock2f.name = "rock2f";

		// Prefab rock f
		GameObject rock3f = Instantiate (rocks[5],  			
							new Vector3 (Random.Range (18.0f, 24.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock3f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock3f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock3f.name = "rock3f";

		// Prefab rock h
		GameObject rock4f = Instantiate (rocks[7],  			
							new Vector3 (laenge/2 - Random.Range (1.0f, 3.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock4f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock4f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock4f.name = "rock4f";


		// Prefab rock f
		GameObject rock5f = Instantiate (rocks[5],  			
							new Vector3 (laenge/2 + Random.Range (5.0f, 9.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock5f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock5f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock5f.name = "rock5f";

		// Prefab rock b
		GameObject rock6f = Instantiate (rocks[1],  			
							new Vector3 (laenge/2 + Random.Range (11.0f, 20.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock6f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock6f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock6f.name = "rock6f";

		// Prefab rock d
		GameObject rock7f = Instantiate (rocks[3],  			
							new Vector3 (laenge - Random.Range (8.0f, 12.0f), 0.0f, -14.0f), 
							Quaternion.Euler(0, Random.Range (0, 360), 0)) 
							as GameObject;
		rock7f.transform.localScale = new Vector3 (Random.Range (4.0f, 5.0f), Random.Range (0.5f, 1.0f), Random.Range (2.0f, 4.0f));
		rock7f.transform.SetParent(GameObject.Find("LittleRocks").transform);
		rock7f.name = "rock7f";	
	}
}