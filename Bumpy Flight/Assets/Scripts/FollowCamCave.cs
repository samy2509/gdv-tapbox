using UnityEngine;

public class FollowCamCave : MonoBehaviour {

	public Transform target;

	void Update () {
		Camera.main.fieldOfView = 45;
	}

	void LateUpdate () {
		Vector3 offset = new Vector3(0f, 6f, -12f);
		Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
		transform.position = softCam;

		transform.LookAt(target);
	}
}
