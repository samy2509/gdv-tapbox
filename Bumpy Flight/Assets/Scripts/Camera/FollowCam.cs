using UnityEngine;

public class FollowCam : MonoBehaviour {

	public Transform target;
	public bool moveCam;
	public float nowPlayerPosition; 
	public float oldPlayerPosition;
	public bool enemyBoss;
	private Vector3 offset = new Vector3(0f, 4f, -16f);
	private float	camTargetY;

	void Start () {
		moveCam = true;
		oldPlayerPosition = 1.0f;
		camTargetY = transform.position.y;
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
			}
		}
		if(nowPlayerPosition < oldPlayerPosition) {
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
				Ray ray = new Ray(GameObject.Find("Chick").transform.position, -(transform.up));
				RaycastHit hit;

				if (moveCam && Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Floor"))) {
					camTargetY = transform.position.y - hit.distance + 1f;
				}

				moveCam = false;
			}
		}

		// Gegner spawned?
		if(Camera.main.transform.position.x % 220 < 1) {
			if(GameObject.Find("Cow large(Clone)") && !enemyBoss) {
				moveCam = false;
				enemyBoss = true;
				Debug.Log("Gegner erkannt");
				offset = new Vector3(-4f, 7f, -16f);
				Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
				transform.position = softCam;
			}
		}
		// Gegner zerstört, normales Game wird fortgesetzt
		if(!GameObject.Find("Cow large(Clone)") && enemyBoss) {
			moveCam = true;
			enemyBoss = false;
			Debug.Log("Gegner ausgelöscht");	
			offset = new Vector3(0f, 4f, -16f);		
			Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
			transform.position = softCam;
			transform.LookAt(target);
		}
	}

	void LateUpdate () {
		if(moveCam == true) {	
			Vector3 softCam = Vector3.Lerp(transform.position, target.position + offset, 0.25f);
			transform.position = softCam;

			transform.LookAt(target);
		} else {
			Vector3 softCam = Vector3.Lerp(transform.position, new Vector3(
																	transform.position.x,
																	camTargetY,
																	transform.position.z), 0.25f);
			transform.position = softCam;

			transform.LookAt(new Vector3(
				transform.position.x,
				target.position.y,
				target.position.z
			));
		}
	}
}
