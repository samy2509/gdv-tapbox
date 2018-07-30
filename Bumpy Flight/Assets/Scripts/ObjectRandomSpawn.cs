using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour {
	public GameObject baum;
	public GameObject baum2;

	public GameObject stein;
	public GameObject hindernis;
	public GameObject gegner;

	private List<GameObject> trees;
	private List<GameObject> stones;
	private List<GameObject> barriers;
	private List<GameObject> enemies;
	
	private float depth 			= 1f;	// Tiefe, in der die Hintergrundobjekte platziert werden
	private float distanceEnemy		= 50f;	// Minimaler Abstand der Gegner
	private float lastPos			= 0f;	// letzte Position eine platzierten Hintergurndobjektes

	// Use this for initialization
	void Start () {
		trees 			= new List<GameObject>();
		stones 			= new List<GameObject>();
		barriers 		= new List<GameObject>();
		enemies 		= new List<GameObject>();
	}

	/*
	*	Platziert Objekte an zufälligen Orten
	*
	*	@pos:	Die Position, an der des Objekt platziert werden soll
	*	@depth:	Die Tiefe der Levelkurve (z-Richtung)
	*/
	public void SpawnObjects( Vector3 pos, float depth ) {
		int rand = Random.Range(0, 5);

		if(depth != this.depth)
			this.depth = depth;

		switch(rand) {
			case 0:
				break;
			case 1:
				SpawnTree(pos, 1f);
				break;
			case 2:
				SpawnTree(pos, 1f);
				break;
			case 3:
				SpawnStone(pos, 3f);
				break;
			case 4:
				SpawnBarrier(pos, 20f);
				break;
		}
	}

	/*
	*	Platziert Gegner an zufälligen Orten
	*
	*	@pos:	Die Position, an der der Gegner platziert werden soll
	*/
	public void SpawnEnemy( Vector3 pos ) {
		int indexE = 0;
		float lastEnemyX;
		float rand = Random.Range(0, 20);

		if(enemies.Count != 0) {
			indexE = enemies.Count - 1;
			lastEnemyX = enemies[indexE].transform.position.x;
		} else {
			lastEnemyX = 0;
		}
		
		Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .3f,
				pos.z + 7
			);

		if(lastEnemyX + distanceEnemy < pos.x && rand == 1f && barriers.Count > 0 && pos.x != barriers[barriers.Count - 1].transform.position.x) {
			GameObject gegnerInst = Instantiate( gegner, newPos, Quaternion.identity ) as GameObject;
			gegnerInst.transform.Rotate( 0f, -90f, 0f );
			enemies.Add( gegnerInst.gameObject );

			gegnerInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
		}
	}

	/*
	*	Platziert Bäume an zufälligen Orten
	*
	*	@pos:		Die Position, an der der Baum platziert werden soll
	*	@distance:	Der minimale Abstand der Bäume
	*/
	private void SpawnTree( Vector3 pos, float distance ) {
		if(LastObjectDistance() + 1.5f + distance <= pos.x) {
			int rand = Random.Range(0, 10);
			GameObject cBaum = baum;
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .3f,
				pos.z - 1 + depth - Random.Range(1f, 5f)
			);

			if(rand != 3) {
				cBaum = baum;
			} else {
				cBaum = baum2;
			}

			GameObject baumInst = Instantiate( cBaum, newPos, Quaternion.identity ) as GameObject;
			baumInst.transform.localScale = new Vector3(3f, 3f, 3f);
			baumInst.transform.Rotate(0f, Random.Range(0, 180), 0f);
			trees.Add( baumInst.gameObject );

			baumInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
		}
	}

	/*
	*	Platziert Hindernisse an zufälligen Orten
	*
	*	@pos:		Die Position, an der das Hindernis platziert werden soll
	*	@distance:	Der minimale Abstand der Hindernisse
	*/
	private void SpawnBarrier( Vector3 pos, float distance ) {
		int rand = Random.Range(0, 7);

		if(lastPos + distance <= pos.x && rand == 3) {
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .5f,
				pos.z + depth/2 - Random.Range(1f, 5f)
			);

			GameObject hindernisInst = Instantiate( hindernis, newPos, Quaternion.identity ) as GameObject;
			hindernisInst.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			barriers.Add( hindernisInst.gameObject );

			hindernisInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);

			lastPos = pos.x;
		}
	}

	/*
	*	Platziert Steine an zufälligen Orten
	*
	*	@pos:		Die Position, an der der Stein platziert werden soll
	*	@distance:	Der minimale Abstand der Steine
	*/
	private void SpawnStone( Vector3 pos, float distance ) {
		if(LastObjectDistance() + distance <= pos.x) {
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .2f,
				pos.z  - 1 + depth - Random.Range(1f, 5f)
			);

			GameObject steinInst = Instantiate( stein, newPos, Quaternion.identity ) as GameObject;
			steinInst.transform.localScale = new Vector3(3f, 3f, 3f);
			steinInst.transform.Rotate(0f, Random.Range(0, 180), Random.Range(0, 20));
			stones.Add( steinInst.gameObject );

			steinInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
		}
	}

	// Gibt den x-Wert des zuletzt platzierten Objektes zurück
	private float LastObjectDistance() {
		float max = 0f;

		if( trees.Count != 0 && trees[trees.Count - 1].transform.position.x > max ) {
			max = trees[trees.Count - 1].transform.position.x;
		}

		if( stones.Count != 0 && stones[stones.Count - 1].transform.position.x > max ) {
			max = stones[stones.Count - 1].transform.position.x;
		}

		return max;
	}
}
