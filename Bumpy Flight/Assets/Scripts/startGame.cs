using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

	public void StartGame() {
		Debug.Log("Loading Level 1...");
		// SceneManager.LoadScene("");
	}

	public void QuitGame() {
		Debug.Log("QUITTED");
		Application.Quit();
	}
}
