using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EiDMG : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("eiStop") || other.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
