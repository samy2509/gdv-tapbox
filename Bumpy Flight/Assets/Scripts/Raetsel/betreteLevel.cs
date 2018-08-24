using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class betreteLevel : MonoBehaviour 
{
	void OnTriggerEnter(Collider col)
    {
        LevelManager lm = GameObject.Find("Player").GetComponent<LevelManager>();

        //Level1
        if (col.tag == "player")
        {
            GameObject.Find("Player").GetComponent<AudioFX>().spawn.Play();
            Debug.Log("Level betreten");

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
            Debug.Log("Level betreten");

            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Highscore") + 100);
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
