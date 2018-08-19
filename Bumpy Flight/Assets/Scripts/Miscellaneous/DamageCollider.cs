using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour {
    public int damageValue = 1;
    public string tag = "Player"; //muss dem Player in Unity zugewiesen werden und im BoxCollider muss "is trigger" aktiviert sein
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
        //GameObject berührt unseren Spieler, über send Message wird die funktion ApplyDamage aufgerufen 
        //dontrequireReceiver verhindert Fehler falls ein Objekt gegen den Collider kommt der keine funktion ApplyDamage Besitzt
        if(other.gameObject.tag == tag) {
            other.gameObject.SendMessage("ApplyDamage", damageValue, SendMessageOptions.DontRequireReceiver);
        }

    }
}
