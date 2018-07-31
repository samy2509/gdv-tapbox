﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateCurve : MonoBehaviour {
	private float 	length 			= 1.0f;				// Länge eines Elements
	private int 	totalDepth 		= 26;				// Vertikale Tiefe des gesamten Bands
	private float 	depth 			= 1.0f;				// Vertikale Tiefe des Bands
	private int 	moves			= 0;				// Anzahl der aktuellen Bewegungen
	private int		rounding		= 5;				// Grad der Abrundung in Kurven
	private float	deviation		= 5f;				// Maximale Abweichung bei der Berechnung des neuen Winkels
	private float	levelBound		= 2f;				// Levelbegrenzung im oberen Bereich
	private float	randomnes		= .5f;				// Zufällige Abweichung von der Höhe in y-Richtung pro Punkt

	private MeshFilter 		meshFilter;
	private Mesh 			mesh;
	private GameObject 		turtle;
	private List<Vector3> 	vertList;
	private List<int> 		triList;
	private List<Vector3>	normalsList;
	private Vector3[]		verts;
	private ObjectRandomSpawn objectSpawner;
	private MeshCollider meshc;

	// Use this for initialization
	void Start () {
		mesh 			= new Mesh();
		vertList 		= new List<Vector3>();
		triList 		= new List<int>();
		normalsList 	= new List<Vector3>();
		turtle 			= new GameObject( "Turtle" );
		objectSpawner	= GetComponent<ObjectRandomSpawn>();

		meshFilter = GetComponent<MeshFilter>();

		meshFilter.mesh = mesh;
		mesh.name = "Boden";

		meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
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
					vertList.Add( new Vector3(  turtle.transform.position.x,
												turtle.transform.position.y + Random.Range(0f, randomnes),
												turtle.transform.position.z + (float)i ));
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
				vertList.Add( new Vector3(  turtle.transform.position.x,
											turtle.transform.position.y + Random.Range(0f, randomnes),
											turtle.transform.position.z + (float)i ));
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

		//RemoveOldVerts( turtle.transform.position.x );

		mesh.RecalculateNormals (); 
		mesh.MarkDynamic ();

		// Zufällige Umgebungsobjekte platzieren
		objectSpawner.SpawnObjects( turtle.transform.position, totalDepth );

		// Gegner platzieren
		objectSpawner.SpawnEnemy( turtle.transform.position );
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

	public void RemoveOldVerts( float currentXPos ) {
		while( true ) {
			if(vertList[0].x < currentXPos - 100) {
				vertList.RemoveAt(0);
			} else {
				return;
			}
		}
	}
}
