using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public GameObject player;
    public GameObject currentCheckpoint;
    public int health;
    public Text healthText;

	// Use this for initialization
	void Start () {
        healthText.text = health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer() {
        //leben abziehen
        health = health - 1;
        //lebensanzeige aktualisieren
        healthText.text = health.ToString();
        //überprüfen ob spieler leben hat
        if (health > 0)
        {
            //wenn ja -> zurück zum checkpoint
            player.transform.position = currentCheckpoint.transform.position;

        }
        else {
            //wenn nein -> spielende
            Debug.Log("Game Over");
            Time.timeScale = 0.0f;
        }

    }
}
