using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script ist Prefabs zugeordnet

public class ObjectDestroyer : MonoBehaviour
{

    void Start()
    {
        // Startet Destroyer nach 0.5 Sekunden, Destroyer prüft dann alle 3 Sekunden
        InvokeRepeating("destroyer", 0.5f, 3.0f);
    }

    void destroyer ()
    {
        // ggf. Distanz (50.0f) anpassen, falls Objekte zu früh/spät zerstört werden
        if (Camera.main.gameObject.transform.position.x > gameObject.transform.position.x + 50.0f)
        {
            Destroy(gameObject);
        }
    }
}
