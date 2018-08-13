using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Text 		scoreText;
	public int 			score;

	// Use this for initialization
	void Start () {
		scoreText.text = "0";
		score = -12;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToScore( int val ) {
		score += val;
		scoreText.text = score.ToString();
	}

	public void GameOn() {
		Time.timeScale = 1f;
	}
}
