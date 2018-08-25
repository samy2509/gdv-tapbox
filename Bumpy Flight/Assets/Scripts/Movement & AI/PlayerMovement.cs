using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public  ParticleSystem      particleLauncher;       // Particle Launcher fürs Fliegen
    private Scene               scene;                  // Um aktuelle Scene zu prüfen

    void Start () {
        scene       = SceneManager.GetActiveScene();
        controller  = gameObject.GetComponent<CharacterController>();
        mSpeed      = 7.0f;
	}

    void Update()
    {
        if (scene.name == "Level1")
        {
            if (!pauseUI.activeSelf)
            {
                InputCheck();
                Move();
            }
        }
        else if (scene.name == "Nebenlevel")
        {
            if (!pauseUI.activeSelf && GameObject.Find("LevelManager").GetComponent<LevelManager>().isWaiting == 0)
            {
                InputCheck();
                Move();
            }
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
            GameObject.Find("Player").GetComponent<AudioFX>().shootEgg.Play();

            GameObject egg = (GameObject)Instantiate(EggPrefab, SpawnPoint.position, Quaternion.identity);
            egg.transform.Rotate(0, 0, 90);
            egg.GetComponent<Rigidbody>().AddForce(Vector3.down * eggSpeed);
            isEgging = false;
        }

    }
    void Move() {
        if (controller.isGrounded) {
            if (inputJump) {
                moveDirection.y = jumpForce;

                GameObject.Find("Player").GetComponent<AudioFX>().flattern.Play();
                particleLauncher.Emit (10);     
            }
        }
        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; 
        controller.Move(moveDirection * Time.deltaTime);
    }

}
