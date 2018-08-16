using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

	// Lädt das erste Level des Spiels
	public void StartGame() {
		Debug.Log("Loading Level 1...");
		Time.timeScale = 1f;
		Cursor.visible = false;
		SceneManager.LoadScene("Level1");
	}

	// Beendet das Spiel
	public void QuitGame() {
		Debug.Log("QUITTED");
		Application.Quit();
	}
}
