using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public GameObject       player;
    private int             powerUp = 0;
    public GameObject       currentCheckpoint;
    public int              health = 4;
    public Text             healthText;
    public GameObject       restartScreen;
    public Text             restartScore;
    private UIController    uicontroller;
    public GameObject       ui;
    public GameObject       pauseUI;
    public GameObject       shieldPrefab;
    private ObjectRandomSpawn ors;

    public  int                 isWaiting;          // Sperrvariable Für Respawn in Höhle
    public  ParticleSystem      particleLauncher;   // Particle Launcher für Damage
    private Scene               scene;              // Um aktuelle Scene zu prüfen
    private AudioFX             audioScript;        // Für Audio-FX
    private CharacterController cc;                 // cc zum Blockieren der Stuerung bei Respawn in Höhle

	// Use this for initialization
	void Start () {
        isWaiting   = 0;
        scene       = SceneManager.GetActiveScene();
        audioScript = GameObject.Find("Player").GetComponent<AudioFX>();
        
        health = PlayerPrefs.GetInt("Health");
        healthText.text = health.ToString();
        
        uicontroller    = GameObject.Find("LevelManager").GetComponent<UIController>();
        if (scene.name == "Level1") 
        {
            ors = GameObject.Find("LevelGenerator").GetComponent<ObjectRandomSpawn>();
        }
        ui = GameObject.Find("UI");

        uicontroller.ReadScore();
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

    void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("leben"))
         {  
             if (health < 4)
             {
                health++;
                healthText.text = health.ToString();
                Debug.Log("Leben aufgesammelt!");
                audioScript.collected.Play();
                ors.DeleteCollectables(other.gameObject);
                Destroy(other.gameObject);
             }
         }
         else if (other.CompareTag("schutzschild"))
         {
             if (powerUp == 0)
             {
                 powerUp++;
                 Debug.Log("Schutzschild aufgesammelt!");
                 audioScript.collected.Play();
                 ors.DeleteCollectables(other.gameObject);
                 Destroy(other.gameObject);

                 // Schild anzeigen
                Vector3 playerPos = GameObject.Find("Player").transform.position;
                playerPos = new Vector3(
                    playerPos.x,
                    playerPos.y + 2,
                    playerPos.z
                );

                GameObject gegnerInst = Instantiate( shieldPrefab, playerPos, Quaternion.identity ) as GameObject;
                gegnerInst.transform.SetParent(GameObject.Find("Player").transform);
             }
         }
     }

    public void RespawnPlayer() {
        //leben abziehen
        if (powerUp != 1) {
            health = health - 1;
        }

        if(powerUp == 1) {
            Destroy(GameObject.Find("shield(Clone)"));
        }

        powerUp = 0;
        //lebensanzeige aktualisieren
        healthText.text = health.ToString();
        //überprüfen ob spieler leben hat
        if (health > 0)
        {
            audioScript.damage.Play();
            // emit 25 particles because of damage
            particleLauncher.Emit (25);

            //wenn ja -> zurück zum checkpoint (Nebenlevel kurze Sperre)
            if (scene.name == "Level1") 
            {
                player.transform.position = currentCheckpoint.transform.position;
            }
            else if (scene.name == "Nebenlevel")  
            {
                StartCoroutine(WaitBeforeSpawn());
            }

            // emit 25 particles because of respawn
            particleLauncher.Emit (25);
        }
        else {
            //wenn nein -> spielende
            if (scene.name == "Level1") 
            {
                audioScript.level1Background.Stop();
            }
            else if (scene.name == "Nebenlevel") 
            {
                audioScript.nebenlevelBackground.Stop();
            }
        
            audioScript.gameover.Play();

            ui.SetActive(false);
            Cursor.visible = true;
            int val = uicontroller.score;
            restartScore.text = val.ToString();
            restartScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }

    IEnumerator WaitBeforeSpawn()
    {
        isWaiting = 1;
        cc = GameObject.FindGameObjectWithTag("player2").GetComponent<CharacterController>();
        cc.enabled = false;
        player.transform.position = currentCheckpoint.transform.position;

        yield return new WaitForSeconds(0.25f);

        cc.enabled = true;
        isWaiting = 0;
    }
}