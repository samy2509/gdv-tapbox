using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public GameObject       player;
    private int             powerUp = 1;
    public GameObject       currentCheckpoint;
    public int              health = 4;
    public Text             healthText;
    public GameObject       restartScreen;
    public Text             restartScore;
    private UIController    uicontroller;
    public GameObject       ui;
    public GameObject       pauseUI;
    public ParticleSystem   particleLauncher;       // Particle Launcher für Damage

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
			} else if(!restartScreen.activeSelf) {
                Cursor.visible = true;
				Time.timeScale = 0.0f;
            	pauseUI.SetActive(true);
			}
        }
	}

   /* void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("leben"))
        {  
            if (health < 4)
            {
                health++;
                Debug.Log("Leben aufgesammelt!");
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("schutzschild"))
        {
            if (powerUp == 0)
            {
                powerUp++;
                Debug.Log("Schutzschild aufgesammelt!");
                Destroy(other.gameObject);
            }
        }
    }*/

    public void RespawnPlayer() {
        //leben abziehen
        if (powerUp != 1) {
            health = health - 1;
        }
        powerUp = 0;
        //lebensanzeige aktualisieren
        healthText.text = health.ToString();
        //überprüfen ob spieler leben hat
        if (health > 0)
        {
            // emit 25 particles because of damage
            particleLauncher.Emit (25);
            //wenn ja -> zurück zum checkpoint
            player.transform.position = currentCheckpoint.transform.position;
            // emit 25 particles because of respawn
            particleLauncher.Emit (25);
        }
        else {
            //wenn nein -> spielende
            ui.SetActive(false);
            Cursor.visible = true;
            int val = uicontroller.score;
            restartScore.text = val.ToString();
            restartScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }
}
