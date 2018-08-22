using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController controller;
    Vector3                     moveDirection   = Vector3.zero;
    public float                mSpeed          = 10.0f;
    private float               gravity         = 44.0f;
    private float               jumpForce       = 24.0f;
    private float               velocity        = 0;
    private bool                inputJump;
    private bool                isEgging        = false;
    public                      GameObject      EggPrefab;
    public                      Transform       SpawnPoint;
    public float                eggSpeed        = 500 ;
    public GameObject           pauseUI;
    public ParticleSystem       particleLauncher;       // Particle Launcher für Jump

    void Start () {
        controller      = gameObject.GetComponent<CharacterController>();
        mSpeed          = 7.0f;
	}

	void Update () {
        if(!pauseUI.activeSelf) {
            InputCheck();
            Move();
        }
        //SetAnimation();

        // Rotation an Boden anpassen
        Ray ray = new Ray(transform.position, -(transform.up));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Floor"))) {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        
    }
    void InputCheck() {
        velocity = Input.GetAxis("Horizontal") * mSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputJump = true;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isEgging) {
            isEgging = true;
        }
        else {
            inputJump = false;
        } 
    }
    void FixedUpdate()
    {
        if (isEgging)
        {
            GameObject egg = (GameObject)Instantiate(EggPrefab, SpawnPoint.position, Quaternion.identity);
            egg.GetComponent<Rigidbody>().AddForce(Vector3.down * eggSpeed);
            isEgging = false;
        }

    }
    void Move() {
        if (controller.isGrounded) {
            if (inputJump) {
                moveDirection.y = jumpForce;
                particleLauncher.Emit (10);     //emit 10 particles
            }
        }
        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; 
        controller.Move(moveDirection * Time.deltaTime);
    }

}
