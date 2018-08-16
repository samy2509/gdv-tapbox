using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {
    public LevelManager levelManager;
    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other){
        if (other.tag == "player") {
            Debug.Log("checkpoint erreicht");
            //checkpoint abspeichern
            levelManager.currentCheckpoint = gameObject;
        }
    }
}
