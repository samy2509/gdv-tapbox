using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script muss Prefabs zugeordnet werden

public class ObjectDestroyer : MonoBehaviour
{

    private float timer = 10.0f;
    //private float wait = 10.0f;

    void Update()
    {

        timer -= Time.deltaTime;

        if (timer < 0.01f && Initialisierung.playingGame == true)
        {
            Destroy(gameObject);
            // while (wait > 0.01f)
            // {
            //     wait -= Time.deltaTime;
            // }
            // wait = 10.0f;
        }
    }
}
