/* 
 * betreteLevel.cs
 *
 * Script für den Wechsel zwischen Level1 und Nebenlevel und umgekehrt.
 * Das Script ist CollPlane der Höhleneingänge und -ausgänge zugewiesen.
 *
 * Funktionen:
 *      OnTriggerEnter(Collider col)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class betreteLevel : MonoBehaviour 
{
    /*  Szenenwechsel On Trigger Enter einleiten.
     * 
     *  @col: Collider that enters the trigger
     */
	void OnTriggerEnter(Collider col)
    {
        LevelManager lm = GameObject.Find("Player").GetComponent<LevelManager>();

        //Level1
        if (col.tag == "player")
        {
            GameObject.Find("Player").GetComponent<AudioFX>().spawn.Play();
            Debug.Log("Nebenlevel betreten");

            PlayerPrefs.SetInt("LastX", 
                (int)GameObject.Find("Turtle").transform.position.x
            );

            PlayerPrefs.SetInt("Health", lm.health);

            SceneManager.LoadScene("Nebenlevel", LoadSceneMode.Single);
        }
        //Nebenlevel
        else if (col.tag == "player2")
        {
            GameObject.Find("Player").GetComponent<AudioFX>().spawn.Play();
            Debug.Log("Level1 betreten");

            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Highscore") + 100);
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}