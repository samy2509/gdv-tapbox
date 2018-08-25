using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAnim : MonoBehaviour {

	public Animator animDuck;
	public CharacterController enemy;
	public float duration = 1; 
	public float startTime = 0f;
	private bool flying = false;

	void Start () {
		animDuck 		= 			GetComponent<Animator>();
		enemy		    = 			gameObject.GetComponent<CharacterController>();
		startTime 		= 			Time.realtimeSinceStartup;
	}
	
	void Update () {
		if(Time.realtimeSinceStartup <= startTime + duration)
     	{
         	transform.position += transform.forward * 1.0f * Time.deltaTime;
         	animDuck.Play("Walk");
     	} else {
         	if(!flying)
         	{
             	animDuck.enabled = false;
             	flying = true;
         	}
     	}

		if (flying == true)
        {
            animDuck.Play("Fly");
            StartCoroutine("StopFlying");
        } else {

		}
    }
     
    IEnumerator StopFlying()
    {
        yield return new WaitForSeconds(1);
        animDuck.enabled = false;
    }

	void ResetTimer()
 	{
     	startTime = Time.realtimeSinceStartup;
     	flying = false;
 	}

}