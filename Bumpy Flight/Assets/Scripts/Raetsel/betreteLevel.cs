using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class betreteLevel : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        //Level1
        if (col.tag == "player")
        {
            Debug.Log("Level betreten");
            SceneManager.LoadScene("Nebenlevel", LoadSceneMode.Single);
        }
        //Nebenlevel
        else if (col.tag == "player2"){
            Debug.Log("Level betreten");
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
