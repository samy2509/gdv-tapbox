using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
	public	int					speed		= 2;	// Geschwindigkeit des Parallax

	private Camera 				cameraMain;			// Haupt-Kamera
	public	GameObject			backgroundParent;	// Der vordere Hintergrund
	public	GameObject			background1;		// Der vordere Hintergrund
	public	GameObject			background2;		// Der hintere Hintergrund
	public	GameObject			background3;		// Der hinterste Hintergrund
	private List<GameObject>	bg1List;			// Liste mit allen vorderen Hintergründen
	private List<GameObject>	bg2List;			// Liste mit allen vorderen Hintergründen
	private	List<GameObject>	bg3List;			// Liste mit allen hinteren Hintergründen
	private float				lastCamX;			// Letzte x-Position der Kamera

	// Use this for initialization
	void Start () {
		cameraMain 		= Camera.main;
		lastCamX 	= cameraMain.transform.position.x;

		bg1List = new List<GameObject>();
		bg2List = new List<GameObject>();
		bg3List = new List<GameObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( cameraMain.transform.position.x != lastCamX ) {
			float diff = cameraMain.transform.position.x - lastCamX;

			for( int i = 0; i < bg1List.Count; i++ ) {
				bg1List[i].transform.position = new Vector3(
					bg1List[i].transform.position.x + (diff / speed / 4),
					bg1List[i].transform.position.y,
					bg1List[i].transform.position.z
				);
			}

			for( int i = 0; i < bg2List.Count; i++ ) {
				bg2List[i].transform.position = new Vector3(
					bg2List[i].transform.position.x + (diff / speed / 2),
					bg2List[i].transform.position.y,
					bg2List[i].transform.position.z
				);
			}

			for( int i = 0; i < bg3List.Count; i++ ) {
				bg3List[i].transform.position = new Vector3(
					bg3List[i].transform.position.x + (diff / speed),
					bg3List[i].transform.position.y,
					bg3List[i].transform.position.z
				);
			}

			lastCamX = cameraMain.transform.position.x;
		}

		// Hintergründe hinzufügen
		SpawnBackground( bg1List );
		SpawnBackground( bg2List );
		SpawnBackground( bg3List );

		// Hintergründe entfernen
		DespawnBackground( bg1List );
		DespawnBackground( bg2List );
		DespawnBackground( bg3List );
	}

	/*
	*	Fügt neue Hintergründe zur Szene hinzu, sobald sie im Sichtfeld sind
	*
	*	@list: Liste, auf die hinzugefügt werden soll
	 */
	public void SpawnBackground ( List<GameObject> list ) {
		GameObject 	background 	= background1;
		float		offsetX		= 0;
		float		offsetY		= 8;
		float		offsetZ		= 40;

		if( list == bg2List ) {
			background 	= background2;
			offsetX		= 10f;
			offsetY		= 12f;
			offsetZ		= 70;
		} else if( list == bg3List ) {
			background 	= background3;
			offsetX		= 25;
			offsetY		= 20;
			offsetZ		= 100;
		}

		if ( list.Count == 0 ) {
			GameObject bg = Instantiate( 
				background, 
				new Vector3(
					9.5f - offsetX,
					-8 + offsetY,
					offsetZ
				), 
				Quaternion.identity );

			bg.transform.Rotate(90, 180, 0);

			list.Add( bg );
			bg.transform.SetParent( backgroundParent.transform );
		} else if( (list.Count > 0 && list[list.Count - 1].transform.position.x < cameraMain.transform.position.x + 40) ) {
			GameObject bg = Instantiate( 
				background, 
				new Vector3(
					list[list.Count - 1].transform.position.x + 95f,
					list[list.Count - 1].transform.position.y,
					offsetZ
				), 
				Quaternion.identity );

			bg.transform.Rotate(90, 180, 0);

			list.Add( bg );
			bg.transform.SetParent( backgroundParent.transform );
		}
	}

	/*
	*	Entfernt alle Hintergründe auf der Liste, die außerhalb des Blickfelds sind
	*
	*	@list: Liste, die geprüft werden soll
	 */
	public void DespawnBackground ( List<GameObject> list ) {
		if( list.Count > 0 && list[0].transform.position.x < cameraMain.transform.position.x - 120 ) {
			Destroy( list[0] );
			list.RemoveAt(0);
		}
	}
}
