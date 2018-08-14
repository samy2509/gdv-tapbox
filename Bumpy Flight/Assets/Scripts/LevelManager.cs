using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public GameObject       player;
    public GameObject       currentCheckpoint;
    public int              health;
    public Text             healthText;
    public GameObject       restartScreen;
    public Text             restartScore;
    private UIController    uicontroller;
    public  GameObject      ui;
    public GameObject       pauseUI;

	// Use this for initialization
	void Start () {
        healthText.text = health.ToString();
        uicontroller	= GameObject.Find("LevelManager").GetComponent<UIController>();
        ui	            = GameObject.Find("UI");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if(pauseUI.activeSelf) {
                Cursor.visible = false;
				Time.timeScale = 1f;
		        pauseUI.SetActive(false);
			} else {
                Cursor.visible = true;
				Time.timeScale = 0.0f;
            	pauseUI.SetActive(true);
			}
        }
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
            ui.SetActive(false);
            int val = uicontroller.score;
            restartScore.text = val.ToString();
            restartScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }
}
