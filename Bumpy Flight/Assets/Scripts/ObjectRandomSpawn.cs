using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour {
	public GameObject baum;
	public GameObject stein;
	public GameObject hindernis;

	private List<GameObject> trees;
	private List<GameObject> stones;
	private List<GameObject> barriers;
	
	private float depth;
	private float lastPos;

	// Use this for initialization
	void Start () {
		trees 		= new List<GameObject>();
		stones 		= new List<GameObject>();
		barriers 	= new List<GameObject>();
		depth 		= 1f;
		lastPos 	= 0f;
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
	*	Platziert Bäume an zufälligen Orten
	*
	*	@pos:		Die Position, an der der Baum platziert werden soll
	*	@distance:	Der minimale Abstand der Bäume
	*/
	private void SpawnTree( Vector3 pos, float distance ) {
		if(LastObjectDistance() + distance <= pos.x) {
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + 2,
				pos.z + depth - Random.Range(1f, 5f)
			);

			GameObject baumInst = Instantiate( baum, newPos, Quaternion.identity ) as GameObject;
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
				pos.y + .5f,
				pos.z + depth - Random.Range(1f, 5f)
			);

			GameObject steinInst = Instantiate( stein, newPos, Quaternion.identity ) as GameObject;
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
