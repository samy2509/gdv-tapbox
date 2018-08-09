using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateCurve : MonoBehaviour {
	private float 	length 			= 1.0f;				// Länge eines Elements
	private int 	totalDepth 		= 26;				// Vertikale Tiefe des gesamten Bands
	private int 	moves			= 0;				// Anzahl der aktuellen Bewegungen
	private int		rounding		= 5;				// Grad der Abrundung in Kurven
	private float	deviation		= 5f;				// Maximale Abweichung bei der Berechnung des neuen Winkels
	private float	levelBound		= 2f;				// Levelbegrenzung im oberen Bereich
	private float	randomnes		= .5f;				// Zufällige Abweichung von der Höhe in y-Richtung pro Punkt
	private int		fovCamera		= 26;				// Bereich, den die Kamera "sieht"
	private int		pathWidth		= 8;				// Breite des Weges
	private int[]   bossSpawns 		= { 300, 500 };		// Bereiche, an denen Bosse spawnen sollen

	private MeshFilter 			meshFilter;
	private Mesh 				mesh;
	public GameObject 			turtle;
	private List<Vector3> 		vertList;
	private List<int> 			triList;
	private List<Vector3>		normalsList;
	private Vector3[]			verts;
	private ObjectRandomSpawn 	objectSpawner;
	private MeshCollider 		meshc;
	private Camera 				mainCamera;

	// Use this for initialization
	void Start () {
		mesh 			= new Mesh();
		vertList 		= new List<Vector3>();
		triList 		= new List<int>();
		normalsList 	= new List<Vector3>();
		turtle 			= new GameObject( "Turtle" );

		objectSpawner	= GetComponent<ObjectRandomSpawn>();

		meshFilter 		= GetComponent<MeshFilter>();
		mainCamera		= Camera.main;

		meshFilter.mesh = mesh;
		mesh.name = "Boden";

		meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( moves == 0 ) {
			for( int i = 0; i < fovCamera ; i++ ) {
				Move( length );
			}

			mesh.vertices 	= vertList.ToArray();
			mesh.normals 	= normalsList.ToArray();
			mesh.triangles 	= triList.ToArray();

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			DestroyImmediate(meshc);
			meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
			meshc.sharedMesh = mesh;
		} else if( mainCamera.transform.position.x + fovCamera > turtle.transform.position.x ) {
			StepForward();
		}
	}

	// Generiert einen neuen Abschnitt des Levels
	public void StepForward() {
		Move( length );
		Turn( GenerateAngle() );
		RemoveOldVerts( turtle.transform.position.x );

		mesh.vertices 	= vertList.ToArray();
		mesh.normals 	= normalsList.ToArray();
		mesh.triangles 	= triList.ToArray();

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		DestroyImmediate(meshc);
		meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
	}

	/*
	*	Rotiert die Kurve um den Winkel angle mit der Rundung rouding
	*
	*	@angle:	Winkel, um den rotiert werden soll
	 */
	public void Turn( float angle ) {
		bool 	negative 	= angle < 0 ? true : false;
		float 	step 		= 0;

		if( negative ) {
			step = -angle / rounding;

			if(step < .7) {
				step = 1;
			}
		} else {
			step = angle / rounding;
			if(step > -.7) {
				step = 1;
			}
		}

		for( int i = 0; i <= step; i++ ) {
			turtle.transform.Rotate(0f, 0f, angle / rounding);
			Move( step * length );
		}
	}

	/*
	*	Bewegt die Kurve um die Länge length
	*
	*	@length:	Länge, um die nach vorne bewegt werden soll
	 */
	public void Move( float length ) {
		if(moves == 0) {
			for (int i = 0; i < totalDepth; i++) {
				if(i != 0) {
					if( i <= 4 || i > 4 + pathWidth ) {
						vertList.Add( new Vector3(  turtle.transform.position.x,
													turtle.transform.position.y + Random.Range(0f, randomnes),
													turtle.transform.position.z + (float)i ));
					} else {
						vertList.Add( new Vector3(  turtle.transform.position.x,
													turtle.transform.position.y + randomnes,
													turtle.transform.position.z + (float)i ));
					}
				} else {
					vertList.Add( new Vector3(  turtle.transform.position.x,
												turtle.transform.position.y - 10,
												turtle.transform.position.z ));

					vertList.Add( new Vector3(  turtle.transform.position.x,
												turtle.transform.position.y + Random.Range(0f, randomnes),
												turtle.transform.position.z ));
				}
			}
		}

		turtle.transform.Translate( length, 0f, 0f);

		for (int i = 0; i < totalDepth; i++) {
			if(i != 0) {
				if( i <= 4 || i > 4 + pathWidth ) {
					vertList.Add( new Vector3(  turtle.transform.position.x,
												turtle.transform.position.y + Random.Range(0f, randomnes),
												turtle.transform.position.z + (float)i ));
				} else {
					vertList.Add( new Vector3(  turtle.transform.position.x,
												turtle.transform.position.y + randomnes,
												turtle.transform.position.z + (float)i ));
				}
			} else {
				vertList.Add( new Vector3(  turtle.transform.position.x,
											turtle.transform.position.y - 10,
											turtle.transform.position.z ));

				vertList.Add( new Vector3(  turtle.transform.position.x,
											turtle.transform.position.y + Random.Range(0f, randomnes),
											turtle.transform.position.z ));
			}
		}

		for (int i = 0; i < totalDepth+2 / 2; i++) {
			triList.Add( moves + totalDepth + 1 );
			triList.Add( moves );
			triList.Add( moves + 1 );

			triList.Add( moves + totalDepth );
			triList.Add( moves );
			triList.Add( moves + totalDepth + 1);

			moves++;
		}

		mesh.RecalculateNormals (); 
		mesh.MarkDynamic ();

		// Zufällige Umgebungsobjekte platzieren
		objectSpawner.SpawnObjects( turtle.transform.position, totalDepth, fovCamera );

		// Gegner platzieren
		objectSpawner.SpawnEnemy( turtle.transform.position );

		// Gegner platzieren
		objectSpawner.SpawnCollectable( turtle.transform.position );

		// Boss platzieren
		if(turtle.transform.position.x > bossSpawns[0] - 1 && turtle.transform.position.x < bossSpawns[0]) {
			objectSpawner.SpawnBoss( turtle.transform.position );
		}
	}

	/*
	*	Berechnet die Normalen für Flat-Shading
	*
	*	@mesh:	Mesh für welches die Normalen berechnet werden sollen
	*/
	public void CalcFlatNormals( Mesh mesh ) {
		Vector3[] vertArray = mesh.vertices;
		Vector3[] normals = new Vector3[vertArray.Length];

		for(int i = 0; i < vertList.Count; i++) {
			Vector3 u = vertList[triList[i+1]] - vertList[triList[i]];
			Vector3 v = vertList[triList[i+2]] - vertList[triList[i]];

			Vector3 normal = Vector3.Cross( u, v ).normalized;

			normals[i] = normal;
		}

		mesh.normals = normals;
	}

	// Generiert einen zufälligen Winkel und gibt diesen zurück
	public float GenerateAngle() {
		float newAngle 	= 0;
		float rand		= Random.Range(-deviation, deviation);

		newAngle = rand;

		if( (turtle.transform.eulerAngles.z + newAngle > 30 && turtle.transform.eulerAngles.z + newAngle < 330)
			|| (turtle.transform.position.y > levelBound && newAngle > 0)
			|| (turtle.transform.position.y < 2 && newAngle < 0) ) {
				
			newAngle = 0;
		}

		return newAngle;
	}

	/*
	*	Entfernt Vertices vom Mesh, die im nicht mehr sichtbaren Bereich sind
	*
	*	@currentXPos:	Position von der aus der Abstand berechnet werden soll
	*/
	public void RemoveOldVerts( float currentXPos ) {
		while ( true ) {
			if( vertList[0].x < currentXPos - fovCamera - 25f ) {
				triList.RemoveRange(0, 6);

				for ( int j = 0; j < triList.Count; j++ ) {
					triList[j] = triList[j] - 1;
				}

				vertList.RemoveAt(0);

				moves--;
			} else {
				return;
			}
		}
	}
}
