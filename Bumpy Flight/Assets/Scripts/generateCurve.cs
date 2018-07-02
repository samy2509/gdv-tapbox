using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateCurve : MonoBehaviour {
	private float 	length 			= 1.0f;				// Länge eines Elements
	private float 	depth 			= 1.0f;				// Vertikale Tiefe des Bands
	private int 	moves			= 0;				// Anzahl der aktuellen Bewegungen
	private int		rounding		= 5;				// Grad der Abrundung in Kurven
	private float	deviation		= 5f;				// Maximale Abweichung bei der Berechnung des neuen Winkels
	private float	levelBound		= 5f;				// Levelbegrenzung im oberen Bereich

	private Mesh 			mesh;
	private GameObject 		turtle;
	private List<Vector3> 	vertList;
	private List<int> 		triList;
	private List<Vector3>	normalsList;

	// Use this for initialization
	void Start () {
		mesh 		= new Mesh();
		vertList 	= new List<Vector3>();
		triList 	= new List<int>();
		normalsList = new List<Vector3>();
		turtle 		= new GameObject( "Turtle" );

		GetComponent<MeshFilter>().mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		Move( length );
		Turn( GenerateAngle() );

		mesh.vertices 	= vertList.ToArray();
		mesh.normals 	= normalsList.ToArray();
		mesh.triangles 	= triList.ToArray();
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
		} else {
			step = angle / rounding;
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
		Vector3 normal;

		for (int i = 0; i < 5; i++) {
			vertList.Add( new Vector3(  turtle.transform.position.x,
										turtle.transform.position.y + Random.Range(0f, 0.1f),
										turtle.transform.position.z + (float)i ));
		}

		turtle.transform.Translate( length, 0f, 0f);

		for (float i = 0f; i < 5f; i++) {
			vertList.Add( new Vector3(  turtle.transform.position.x,
										turtle.transform.position.y + Random.Range(0f, 0.1f),
										turtle.transform.position.z + (float)i ));
		}

		for (int i = 0; i < 5; i++) {
			triList.Add( moves + 5 );
			triList.Add( moves );
			triList.Add( moves + 1 );

			Vector3 s = vertList[moves] - vertList[moves + 5];
			Vector3 t = vertList[moves + 1] - vertList[moves + 5];

			normal = Vector3.Cross( s, t ).normalized;

			normalsList.Add( normal );
			normalsList.Add( normal );

			triList.Add( moves + 5 );
			triList.Add( moves + 1 );
			triList.Add( moves + 6 );

			Vector3 u = vertList[moves + 5] - vertList[moves + 1];
			Vector3 v = vertList[moves + 6] - vertList[moves + 1];

			normal = Vector3.Cross( u, v ).normalized;

			normalsList.Add( normal );
			normalsList.Add( normal );

			moves++;
		}
	}

	// Generiert einen zufälligen Winkel und gibt diesen zurück
	public float GenerateAngle() {
		float newAngle 	= 0;
		float rand 		= Random.value;

		if( rand < 0.5f ) {
			newAngle = deviation * -rand * 2;
		} else {
			newAngle = deviation * rand;
		}

		if( (turtle.transform.eulerAngles.z + newAngle > 30 && turtle.transform.eulerAngles.z + newAngle < 330)
			|| (turtle.transform.position.y > levelBound && newAngle > 0)
			|| (turtle.transform.position.y < 2 && newAngle < 0) ) {
				
			newAngle = -newAngle;
		}

		return newAngle;
	}
}
