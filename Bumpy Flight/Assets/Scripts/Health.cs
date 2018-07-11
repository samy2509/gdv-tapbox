using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float currentHealth = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ApplyDamage(float damage) {

        if (currentHealth > 0) {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            if (currentHealth == 0)
            {
                //GameOver neustart der scene
                RestartScene();
            }
        }
        

    }

    //Restartet das Level
    void RestartScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
