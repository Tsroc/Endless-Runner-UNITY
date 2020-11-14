using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Rigidbody rb;
    private Animator animator;
    private GameManager gameManager;

    public float movementSpeed = 10.0f;
    public float xMovementSpeed = 5.0f;
    private float jumpVelocity = 5.0f; 
    private float yaw = 15.0f;
    private float screenClamp = 4.8f;

    private bool grounded = true;
    private bool hasEnergy = true;
    
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(hasEnergy)
        {
            animator.speed = 1.2f;								
            animator.SetFloat ("Speed", movementSpeed);							
            ProcessMovement();
            ProcessJump();
            ProcessRotation();
        }
        else
        {
            animator.SetFloat ("Speed", 0);							
            // Call function end the game.
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Used for clamping the player: https://community.gamedev.tv/t/mathf-clamp-doesnt-quite-clamp-correctly/27694
        StartCoroutine(WaitForFixed());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpawnTrigger"){
            spawnManager.SpawnTriggerEntered();
        }
        if (other.tag == "Obstacle"){
            // If obstacle, what happens?
        Debug.Log("Obstacle collision");
        }
        if(other.tag == "Ground"){
            grounded = true;
        }
    }

    //private void processMovement()
    private void ProcessMovement()
    {
        float hMovement = Input.GetAxis("Horizontal") * xMovementSpeed;

        transform.Translate(new Vector3(hMovement, 0, movementSpeed) * Time.deltaTime);
        //rb.AddForce(hMovement*30, 0, movementSpeed*20);
    }

    IEnumerator WaitForFixed()
    {
        yield return new WaitForFixedUpdate();
        transform.position = ClampingMethod();
    }

    private Vector3 ClampingMethod()
    {
        return new Vector3(Mathf.Clamp(transform.position.x, -screenClamp, screenClamp), transform.position.y, transform.position.z);
    }

    private void ProcessJump(){
        if(Input.GetKeyDown("space")){
            if(grounded == true){
                // Allowed to jump.
                grounded = false;
                rb.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.Impulse);
                animator.Play("Jump");
            }
        }
    }

    private void ProcessRotation()
    {
        // Feels clunky. Return to this!
        if (Input.GetAxis("Horizontal") > 0)    // Moves Right.
            transform.localRotation = Quaternion.Euler(0, yaw, 0);
        else if (Input.GetAxis("Horizontal") < 0)   // Moves Left.
            transform.localRotation = Quaternion.Euler(0, -yaw, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void EnergyDepleted()
    {
        hasEnergy = false;
        gameManager.SendMessage("GameOver");
        //Debug.Log("Energy depleted.");
    }
}
