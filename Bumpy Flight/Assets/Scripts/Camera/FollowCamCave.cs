using UnityEngine;

public class FollowCamCave : MonoBehaviour {

	public Transform target;
	public bool moveCam;
	public float nowPlayerPosition; 
	public float oldPlayerPosition;

	void Start () {
		moveCam = true;
		oldPlayerPosition = 1.0f;
	}

	void Update () {
		Camera.main.fieldOfView = 45;

		nowPlayerPosition = GameObject.Find("Chick").transform.position.x;
	}

	void LateUpdate () {
		if(moveCam == true) {	
			Vector3 offset = new Vector3(0f, 6f, -12f);
			Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
			transform.position = softCam;

			transform.LookAt(target);
		}
	}
}
