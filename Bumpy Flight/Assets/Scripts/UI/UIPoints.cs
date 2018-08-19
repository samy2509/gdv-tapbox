using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour {
	private TextMesh	pText;			// Text, der angezeigt werden soll
	public  float		speed	= 1f;	// Geschwindigkeit mit der der Text steigen soll
	private int			alpha	= 255;

	// Use this for initialization
	void Start () {
		pText = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(
			gameObject.transform.position.x,
			gameObject.transform.position.y + (speed++ / 100),
			gameObject.transform.position.z
		);

		if(alpha > 5) {
			alpha -= 5;
			GetComponent<TextMesh>().color = new Color(255 , 255, 255, alpha);
		} else {
			Destroy(gameObject);
		}
	}

	/*
	*	Spawnt einen Text, der die Punktzahl anzeigt und nach oben steigt
	*
	*	@amount:	Anzahl der Punkte
	*	@position:	Position des Textes
	 */
	public void Points( int amount, Vector3 position ) {
		pText	= gameObject.GetComponent<TextMesh>();
		pText.text = "+" + amount.ToString();
		pText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		pText.fontSize = 20;
		pText.transform.position = position;
	}
}
