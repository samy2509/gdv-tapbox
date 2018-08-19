using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMenu : MonoBehaviour {

	private float 	length 			= 1.0f;				// Länge eines Elements
	private int 	totalDepth 		= 26;				// Vertikale Tiefe des gesamten Bands
	private int 	moves			= 0;				// Anzahl der aktuellen Bewegungen
	private int		rounding		= 5;				// Grad der Abrundung in Kurven
	private float	deviation		= 5f;				// Maximale Abweichung bei der Berechnung des neuen Winkels
	private float	levelBound		= 2f;				// Levelbegrenzung im oberen Bereich
	private float	randomnes		= .5f;				// Zufällige Abweichung von der Höhe in y-Richtung pro Punkt
	private int		fovCamera		= 26;				// Bereich, den die Kamera "sieht"
	private int		pathWidth		= 8;				// Breite des Weges

	private generateCurve curve;
	private bool generated;
	private MeshFilter 			meshFilter;
	private Mesh 				mesh;
	public GameObject 			turtle;
	private List<Vector3> 		vertList;
	private List<int> 			triList;
	private List<Vector3>		normalsList;
	private Vector3[]			verts;
	private MeshCollider 		meshc;
	private ObjectRandomSpawn 	objectSpawner;

	// Use this for initialization
	void Start () {
		// curve = GetComponent<generateCurve>();
		mesh 			= new Mesh();
		vertList 		= new List<Vector3>();
		triList 		= new List<int>();
		normalsList 	= new List<Vector3>();
		turtle 			= new GameObject( "Turtle" );

		objectSpawner	= GetComponent<ObjectRandomSpawn>();
		meshFilter 		= GetComponent<MeshFilter>();

		meshFilter.mesh = mesh;
		mesh.name = "Boden";
		generated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( generated == false ) {
			for( int i = 0; i < 120 ; i++ ) {
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

		// 	for(int i = 0; i < 5; i++) {
		// 		curve.StepForward();
		// 	}

			generated = true;
		}
	}

		// Generiert einen neuen Abschnitt des Levels
	public void StepForward() {
		Move( length );
		Turn( GenerateAngle() );

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
}
