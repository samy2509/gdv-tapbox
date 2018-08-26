using UnityEngine;
using System.Collections;

public class lightFlicker : MonoBehaviour {
	
	Light torchLight;
	public float minTime = 0.2f;
	public float maxTime = 0.5f;
	
	void Start () {
		torchLight = GetComponent<Light>();
		StartCoroutine(Flash());
	}
	
	IEnumerator Flash ()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minTime,maxTime));
			torchLight.enabled = ! torchLight.enabled;

		}
	}
}