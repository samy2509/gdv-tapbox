using UnityEngine;

public class FollowCam : MonoBehaviour {

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

		if(nowPlayerPosition > oldPlayerPosition) {
			oldPlayerPosition = GameObject.Find("Chick").transform.position.x;
		}

		if(nowPlayerPosition >= oldPlayerPosition) {
			if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey("space") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ) {
				moveCam = true;
				Debug.Log("groesser");
			}
		}
		if(nowPlayerPosition < oldPlayerPosition) {
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
				moveCam = false;
			}
		}

		/*in die totale fahren lassen. in generateCurve in dem Array bossSpawns (int[]) , eine Instanz von generateCurve erstellst un dann mit dem punkt-Operator */
	}

	void LateUpdate () {
		if(moveCam == true) {	
			Vector3 offset = new Vector3(0f, 4f, -16f);
			Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
			transform.position = softCam;

			transform.LookAt(target);
		}
	}
}
