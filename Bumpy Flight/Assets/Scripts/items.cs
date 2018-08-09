using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class items : MonoBehaviour {
    private int itemScore;
    public Text itemText;
    public int health;
    public Text healthText;
    private int powerUp;
    public GameObject player;
    public GameObject currentCheckpoint;
    // Use this for initialization
    void Start () {
        healthText.text = health.ToString();
        itemScore = 0;
        itemText.text = itemScore.ToString();
        powerUp = 0;
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            itemScore++;
            Destroy(other.gameObject);
            itemText.text = itemScore.ToString();
        } else if (other.gameObject.tag == "blitzschlag") {
            powerUp = 1;
            Debug.Log("Blitzschlag aufgesammelt!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "leben") {
            Destroy(other.gameObject);
            if (health < 4) {
                health++;
                Debug.Log("Schutzschild aufgesammelt!");
            }
        }
        else if (other.gameObject.tag == "schutzschild") {
            Debug.Log("Schutzschild aufgesammelt!");
        }
    }

    public void RespawnPlayer()
    {
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
        else
        {
            //wenn nein -> spielende
            Debug.Log("Game Over");
            Time.timeScale = 0.0f;
        }

    }
}
