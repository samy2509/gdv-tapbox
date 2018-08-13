using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomSpawn : MonoBehaviour {
	public GameObject baum;				// Baum 1 für Spawn
	public GameObject baum2;			// Baum 2 für Spawn
	public GameObject baum3;			// Baum 3 für Spawn

	public GameObject stein;			// Stein für Spawn
	public GameObject busch;			// Busch für Spawn
	public GameObject hindernis;		// Hindernis für Spawn
	public GameObject gegner;			// Gegner für Spawn
	public GameObject gegner2;			// Schwerer Gegner für Spawn
	public GameObject gegner3;			// Weiterer Schwerer Gegner für Spawn
	public GameObject boss;				// Endgegner für Spawn

	public GameObject leben;			// Collectable für Spawn
	public GameObject schutzschild;		// Collectable für Spawn
	public GameObject blitzschlag;		// Collectable für Spawn
	public GameObject eingang;			// Hoehleneingang für Spawn

	private List<GameObject> trees;			// Liste mit allen Bäumen
	private List<GameObject> stones;		// Liste mit allen Steinen
	private List<GameObject> bushes;		// Liste mit allen Büschen
	private List<GameObject> barriers;		// Liste mit allen Hindernissen
	private List<GameObject> enemies;		// Liste mit allen Gegnern
	private List<GameObject> collectables;	// Liste mit allen Collectables
	private List<GameObject> caves;			// Liste mit allen Hoehleneingaengen
	
	private float depth 			= 6f;	// Tiefe, in der die Hintergrundobjekte platziert werden
	private float distanceEnemy		= 50f;	// Minimaler Abstand der Gegner
	private float lastPos			= 0f;	// letzte Position eine platzierten Hintergurndobjektes
	private float  fov				= 30f;	// Field of View der Kamera

	// Abschnitte für Gegnersteigerung
	public int[] levels = {
		200,
		350,
		500
	};

	// Use this for initialization
	void Start () {
		trees 			= new List<GameObject>();
		stones 			= new List<GameObject>();
		bushes 			= new List<GameObject>();
		barriers 		= new List<GameObject>();
		enemies 		= new List<GameObject>();
		collectables	= new List<GameObject>();
		caves			= new List<GameObject>();

		new GameObject("Gegner");
		new GameObject("Collectables");
		new GameObject("Caves");
	}

	/*
	*	Platziert Objekte an zufälligen Orten
	*
	*	@pos:	Die Position, an der des Objekt platziert werden soll
	*	@depth:	Die Tiefe der Levelkurve (z-Richtung)
	*	@fov:	Field of View der Kamera
	*/
	public void SpawnObjects( Vector3 pos, float depth, float fov ) {
		int rand = Random.Range(0, 5);
		this.fov = fov;

		Destroyer();

		if(depth != this.depth)
			this.depth = depth;

		switch(rand) {
			case 0:
				break;
			case 1:
				SpawnTree(pos, 2f);
				break;
			case 2:
				SpawnTree(pos, 4f);
				break;
			case 3:
				SpawnStone(pos, 7f);
				break;
			case 4:
				SpawnBarrier(pos, 10f);
				break;
		}
	}

	/*
	*	Platziert Gegner an zufälligen Orten
	*
	*	@pos:	Die Position, an der der Gegner platziert werden soll
	*/
	public void SpawnEnemy( Vector3 pos ) {
		GameObject geg = gegner;
		int indexE = 0;
		float lastEnemyX;
		float offsetGegner2 = 4f;
		float rand = Random.Range(0, 10);
		int minRange  = 0;
		int randRange = 2;

		if (pos.x > levels[2] && minRange != 3) {
			minRange = 3;
		} else if(pos.x > levels[1] && randRange != 7) {
			randRange = 7;
		} else if (pos.x > levels[0] && randRange != 4) {
			randRange = 4;
		}

		int randEnemy = Random.Range(minRange, randRange);

		if(enemies.Count != 0) {
			indexE = enemies.Count - 1;
			lastEnemyX = enemies[indexE].transform.position.x;
		} else {
			lastEnemyX = 0;
		}
		
		if(randEnemy <= 2 || randEnemy >= 4) {
			offsetGegner2 = 0;
		}

		if( randEnemy > 2 && randEnemy < 4 ) {
			geg = gegner2;
		} else if ( randEnemy >= 4 ) {
			geg = gegner3;
		}

		Vector3 newPos = new Vector3(
				pos.x,
				pos.y + 1.3f + offsetGegner2,
				pos.z + depth/2 - 4.2f
			);

		if( ((lastEnemyX + distanceEnemy < pos.x && rand == 1f) || lastEnemyX - pos.x > 30) && barriers.Count > 0 && (barriers[barriers.Count - 1].transform.position.x > pos.x + 8 || barriers[barriers.Count - 1].transform.position.x < pos.x - 8 ) ) {
			GameObject gegnerInst = Instantiate( geg, newPos, Quaternion.identity ) as GameObject;
			if( randEnemy >= 4 ) {
				gegnerInst.AddComponent<MovementDuck>();
			} else {
				gegnerInst.AddComponent<Movement>();
			}
			
            gegnerInst.AddComponent<eenemy>();

			if( randEnemy <= 2 ) {
				CharacterController cc = gegnerInst.AddComponent<CharacterController>();
				BoxCollider bc = gegnerInst.AddComponent<BoxCollider>();

				gegnerInst.transform.Rotate( 0f, -90f, 0.11f );
				bc.size = new Vector3(1.73f, 2.27f, 3.27f);
				bc.center = new Vector3(0f, 1.13f, 0f);
				bc.isTrigger = true;
				cc.radius = 1.04f;
				cc.height = 1.91f;
				cc.center = new Vector3(0f, 1.04f, 0f);
			} else if ( randEnemy >= 4) {
				CharacterController cc = gegnerInst.AddComponent<CharacterController>();
				BoxCollider bc = gegnerInst.AddComponent<BoxCollider>();

				gegnerInst.transform.Rotate( 0f, -90f, 0f );
				bc.size = new Vector3(1.73f, 1.65f, 1.47f);
				bc.center = new Vector3(-1.31f, 0.83f, -0.07f);
				bc.isTrigger = true;
				cc.radius = 0.7f;
				cc.height = 1.03f;
				cc.center = new Vector3(0f, 0.71f, -0.07f);
			} else {
				gegnerInst.transform.Rotate( 0f, -90f, 0f );
			}

			enemies.Add( gegnerInst.gameObject );

			gegnerInst.transform.SetParent(GameObject.Find("Gegner").transform);
		}
	}

	/*
	*	Platziert Endgegner an Orten
	*
	*	@pos:	Die Position, an der der Gegner platziert werden soll
	*/
	public void SpawnBoss( Vector3 pos ) {
		GameObject geg = boss;
		int minRange  = 0;
		int randRange = 2;

		if (pos.x > levels[2] && minRange != 3) {
			minRange = 3;
		} else if(pos.x > levels[1] && randRange != 7) {
			randRange = 7;
		} else if (pos.x > levels[0] && randRange != 4) {
			randRange = 4;
		}

		int randEnemy = Random.Range(0, 2);


		if ( randEnemy == 2 ) {
			geg = boss;
		}

		Vector3 newPos = new Vector3(
				pos.x,
				pos.y + 1.3f,
				pos.z + depth/2 - 4.2f
			);

		
		GameObject gegnerInst = Instantiate( geg, newPos, Quaternion.identity ) as GameObject;
		gegnerInst.AddComponent<Movement>();
		gegnerInst.AddComponent<eenemy>();

		if( randEnemy <= 1 ) {
			CharacterController cc = gegnerInst.AddComponent<CharacterController>();
			BoxCollider bc = gegnerInst.AddComponent<BoxCollider>();

			gegnerInst.transform.Rotate( 0f, -90f, 0.11f );
			bc.size = new Vector3(1.73f, 2.27f, 3.27f);
			bc.center = new Vector3(0f, 1.13f, 0f);
			bc.isTrigger = true;
			cc.radius = 1.04f;
			cc.height = 1.91f;
			cc.center = new Vector3(0f, 1.04f, 0f);
		} else if ( randEnemy >= 2) {
			CharacterController cc = gegnerInst.AddComponent<CharacterController>();
			BoxCollider bc = gegnerInst.AddComponent<BoxCollider>();

			gegnerInst.transform.Rotate( 0f, -90f, 0f );
			bc.size = new Vector3(1.73f, 1.65f, 1.47f);
			bc.center = new Vector3(-1.31f, 0.83f, -0.07f);
			bc.isTrigger = true;
			cc.radius = 0.7f;
			cc.height = 1.03f;
			cc.center = new Vector3(0f, 0.71f, -0.07f);
		} else {
			gegnerInst.transform.Rotate( 0f, -90f, 0f );
		}

		gegnerInst.tag = "Boss";

		enemies.Add( gegnerInst.gameObject );

		gegnerInst.transform.SetParent(GameObject.Find("Gegner").transform, false);
	}

    /*
	*	Platziert Collectables an zufälligen Orten
	*
	*	@pos:	Die Position, an der Collectable platziert werden soll
	*/
    public void SpawnCollectable(Vector3 pos)
    {
		float lastCollectX;
		int indexC 				= 0;
		float rand 				= Random.Range(0, 20);
		float distanceCollect 	= Random.Range(200f, 350f);     // Minimaler Abstand der Collectables

        if (collectables.Count != 0)
        {
            indexC = collectables.Count - 1;
            lastCollectX = collectables[indexC].transform.position.x;
        }
        else
        {
            lastCollectX = 0;
        }

        Vector3 newPos = new Vector3(
                pos.x,
                pos.y + Random.Range(-2.0f, 2.5f),
                pos.z + depth / 2 - 6.37f
        );

        if (lastCollectX + distanceCollect < pos.x && rand == 1f && barriers.Count > 0 && pos.x != barriers[barriers.Count - 1].transform.position.x)
        {
            GameObject collectInst = null;
			int randCollect = Random.Range(0, 3);
            switch (randCollect)
            {
                case 0:
                    collectInst = Instantiate(leben, newPos, Quaternion.identity) as GameObject;
					collectInst.tag = "leben";
                    break;
                case 1:
                    collectInst = Instantiate(schutzschild, newPos, Quaternion.identity) as GameObject;
					collectInst.tag = "schutzschild";
                    break;
                case 2:
                    collectInst = Instantiate(blitzschlag, newPos, Quaternion.identity) as GameObject;
					collectInst.tag = "blitzschlag";
                    break;
            }
            
            BoxCollider bc = collectInst.AddComponent<BoxCollider>();
            bc.isTrigger = true;
            bc.size = new Vector3(1.0f, 1.0f, 1.0f);
            bc.center = new Vector3(-1.22f, 5.22f, 2.14f);

            collectables.Add(collectInst.gameObject);

            collectInst.transform.SetParent(GameObject.Find("Collectables").transform);
        }
    }

    /*
	*	Platziert Höhleneingänge an zufälligen Orten
	*
	*	@pos:	Die Position, an der Höhleneingang platziert werden soll
	*/
    public void SpawnCave(Vector3 pos)
    {
        GameObject cave = eingang;

        float lastCaveX;
        int indexC 			= 0;
        float rand 			= Random.Range(0, 20);
        float distanceCave 	= 200f;

        if (caves.Count != 0)
        {
            indexC = caves.Count - 1;
            lastCaveX = caves[indexC].transform.position.x;
        }
        else
        {
            lastCaveX = 0;
        }

        Vector3 newPos = new Vector3(
                pos.x,
                pos.y + 0.8f,
                pos.z
        );

        if (lastCaveX + distanceCave < pos.x && rand == 1f && barriers.Count > 0 && pos.x != barriers[barriers.Count - 1].transform.position.x)
        {
            GameObject caveInst = Instantiate(cave, newPos, Quaternion.identity) as GameObject;
            caves.Add(caveInst.gameObject);
            caveInst.transform.SetParent(GameObject.Find("Caves").transform);
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
			float randScale = Random.Range(2.5f, 3.5f);
			GameObject cBaum = baum;
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .3f,
				pos.z - 1 + depth - Random.Range(1f, 10f)
			);

			if(rand != 3) {
				if(rand > 3) {
					cBaum = baum;
				} else {
					cBaum = baum3;
				}
			} else {
				cBaum = baum2;
			}

			GameObject baumInst = Instantiate( cBaum, newPos, Quaternion.identity ) as GameObject;
			baumInst.transform.localScale = new Vector3(randScale, randScale, randScale);
			baumInst.transform.Rotate(0f, Random.Range(0, 180), 0f);
			trees.Add( baumInst.gameObject );

			baumInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);

			if(rand == 1) {
				newPos = new Vector3(
					pos.x,
					pos.y + .3f,
					pos.z + 1
				);

				GameObject baumVGInst = Instantiate( cBaum, newPos, Quaternion.identity ) as GameObject;
				baumVGInst.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
				baumVGInst.transform.Rotate(0f, Random.Range(0, 180), 0f);
				trees.Add( baumVGInst.gameObject );

				baumVGInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
			}
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

		if((lastPos + distance <= pos.x && rand == 3) || lastPos - pos.x > 50) {
			Vector3 newPos = new Vector3(
				pos.x,
				pos.y + .6f,
				pos.z + depth/2 - Random.Range(1f, 3f) -1
			);

			GameObject hindernisInst = Instantiate( hindernis, newPos, Quaternion.identity ) as GameObject;
			hindernisInst.transform.eulerAngles = new Vector3(-150f, Random.Range(70f, 100f), -90f);
			hindernisInst.transform.localScale = new Vector3(3f, 3f, 3f);
			barriers.Add( hindernisInst.gameObject );
			Rigidbody rb = hindernisInst.AddComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

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
		float randScale = Random.Range(2f, 10f);
		int rand = Random.Range(0, 2);
		if(LastObjectDistance() + distance <= pos.x) {
			if(rand == 0) {
				Vector3 newPos = new Vector3(
					pos.x,
					pos.y + .2f,
					pos.z  - 1 + depth - Random.Range(1f, 5f)
				);

				GameObject steinInst = Instantiate( stein, newPos, Quaternion.identity ) as GameObject;
				steinInst.transform.localScale = new Vector3(randScale, randScale, randScale);
				steinInst.transform.Rotate(0f, Random.Range(0, 180), Random.Range(0, 20));
				stones.Add( steinInst.gameObject );

				steinInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
			} else {
				Vector3 newPos = new Vector3(
					pos.x,
					pos.y + .2f,
					pos.z  - 1 + depth - Random.Range(1f, 5f)
				);

				GameObject buschInst = Instantiate( busch, newPos, Quaternion.identity ) as GameObject;
				buschInst.transform.localScale = new Vector3(randScale - 1, randScale - 1, randScale - 1);
				buschInst.transform.Rotate(0f, Random.Range(0, 180), 0f);
				stones.Add( buschInst.gameObject );

				buschInst.transform.SetParent(GameObject.Find("LevelGenerator").transform);
			}
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

	// Entfernt alle Hindernisse, die nicht mehr innerhalb des FOV sind
	public void Destroyer() {
		bool modified = true;
		GameObject buffer;

		while (modified) {
			modified = false;
			if ( trees.Count > 0  && Camera.main.transform.position.x > trees[0].transform.position.x + fov ) {
				buffer = trees[0];
				trees.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}
			
			if( bushes.Count > 0 && Camera.main.transform.position.x > bushes[0].transform.position.x + fov ) {
				buffer = bushes[0];
				bushes.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}
			
			if( stones.Count > 0 && Camera.main.transform.position.x > stones[0].transform.position.x + fov ) {
				buffer = stones[0];
				stones.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}

			if( barriers.Count > 0 && Camera.main.transform.position.x > barriers[0].transform.position.x + fov ) {
				buffer = barriers[0];
				barriers.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}

			if( enemies.Count > 0 && Camera.main.transform.position.x > enemies[0].transform.position.x + fov ) {
				buffer = enemies[0];
				enemies.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}

			if( collectables.Count > 0 && Camera.main.transform.position.x > collectables[0].transform.position.x + fov ) {
				buffer = collectables[0];
				collectables.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}

			if( caves.Count > 0 && Camera.main.transform.position.x > caves[0].transform.position.x + fov ) {
				buffer = caves[0];
				caves.RemoveAt(0);
				Destroy(buffer);

				if(!modified)
					modified = true;
			}
		}
	}
}
