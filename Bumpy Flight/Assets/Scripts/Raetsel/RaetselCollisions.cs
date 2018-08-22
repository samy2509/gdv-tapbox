using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaetselCollisions : MonoBehaviour {

	private Raetsel 		raetselScript;		//Script Raetsel
	private LevelManager 	levelManagerScript;	//Script LevelManager
	private GameObject 		saeule1;			//Säule1, die den Weg versperrt
	private GameObject 		saeule2;			//Säule2, die den Weg versperrt
	private int[] 			riddle;				//Zufällig gefülltes Array für TouchRockRiddle aus Raetsel
	private int[] 			order;				//orderList aus Raetsel als Array
	
    void Start()
    {
        raetselScript 			= GameObject.Find("LevelManager").GetComponent<Raetsel>();
		levelManagerScript 		= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        riddle					= raetselScript.riddle;
		raetselScript.sperre 	= 0;
    }

    void OnTriggerEnter(Collider col)
    {
		//Rätsel 0 - TouchRockRiddle
        if (raetselScript.rand == 0)
        {
            if (col.gameObject.tag == "player2" && gameObject.name == "detect1")
            {
                if (raetselScript.orderList.Count < 3 && raetselScript.set1 == 0)
                {
                    raetselScript.orderList.Add(0);
                    raetselScript.set1 = 1;
                    GameObject.Find("RedLamp1").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("YellowLamp1").GetComponent<MeshRenderer>().enabled = true;
                }

                checkCorrectness();

                if (raetselScript.orderList.Count == 1)
                {
                    GameObject.Find("RedLamp2").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("RedLamp3").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp2").GetComponent<MeshRenderer>().enabled = true;
                    GameObject.Find("WhiteLamp3").GetComponent<MeshRenderer>().enabled = true;
                }
            }

            if (col.gameObject.tag == "player2" && gameObject.name == "detect2")
            {
                if (raetselScript.orderList.Count < 3 && raetselScript.set2 == 0)
                {
                    raetselScript.orderList.Add(1);
                    raetselScript.set2 = 1;
                    GameObject.Find("RedLamp2").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp2").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("YellowLamp2").GetComponent<MeshRenderer>().enabled = true;
                }

                checkCorrectness();

                if (raetselScript.orderList.Count == 1)
                {
                    GameObject.Find("RedLamp1").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("RedLamp3").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = true;
                    GameObject.Find("WhiteLamp3").GetComponent<MeshRenderer>().enabled = true;
                }
            }

            if (col.gameObject.tag == "player2" && gameObject.name == "detect3")
            {
                if (raetselScript.orderList.Count < 3 && raetselScript.set3 == 0)
                {
                    raetselScript.orderList.Add(2);
                    raetselScript.set3 = 1;
                    GameObject.Find("RedLamp3").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp3").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("YellowLamp3").GetComponent<MeshRenderer>().enabled = true;
                }

                checkCorrectness();

                if (raetselScript.orderList.Count == 1)
                {
                    GameObject.Find("RedLamp1").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("RedLamp2").GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = true;
                    GameObject.Find("WhiteLamp2").GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

		//Hindernis 1 - FireInTheCave
		if (raetselScript.rand == 1) 
		{
			if (col.gameObject.tag == "player2" && gameObject.name == "ColFirePlane")
            {
					levelManagerScript.RespawnPlayer();				
            }
		}
    }

    void openDoors()
    {
        saeule1 = GameObject.Find("Säule1");
        saeule2 = GameObject.Find("Säule2");

        Vector3 newpos1 = saeule1.transform.position;
        newpos1.y -= 5.0f;
        saeule1.transform.position = newpos1;

        Vector3 newpos2 = saeule2.transform.position;
        newpos2.y -= 5.0f;
        saeule2.transform.position = newpos2;

		GameObject.Find("YellowLamp1").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find("YellowLamp2").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find("YellowLamp3").GetComponent<MeshRenderer>().enabled = false;

		GameObject.Find("GreenLamp1").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("GreenLamp2").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("GreenLamp3").GetComponent<MeshRenderer>().enabled = true;

		raetselScript.sperre = 1;
    }

    void checkCorrectness()
    {
        if (raetselScript.orderList.Count > 2)
        {
            order = raetselScript.orderList.ToArray();

            if (riddle[0] != order[0] || riddle[1] != order[1] || riddle[2] != order[2])
            {
                for (int i = 2; i >= 0; i--)
                {
                    raetselScript.orderList.RemoveAt(i);
                }

                raetselScript.set1 = 0;
                raetselScript.set2 = 0;
                raetselScript.set3 = 0;

                GameObject.Find("YellowLamp1").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("YellowLamp2").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("YellowLamp3").GetComponent<MeshRenderer>().enabled = false;

                GameObject.Find("WhiteLamp1").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("WhiteLamp2").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("WhiteLamp3").GetComponent<MeshRenderer>().enabled = false;

                GameObject.Find("RedLamp1").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("RedLamp2").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("RedLamp3").GetComponent<MeshRenderer>().enabled = true;
            }
            else if (raetselScript.sperre == 0)
            {
                openDoors();
            }
        }
    }
}