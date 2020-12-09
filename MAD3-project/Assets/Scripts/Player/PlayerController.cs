using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
        PlayerController manages the controls available to the player, along with collision management.
    */

    private SpawnManager spawnManager;
    private Rigidbody rb;
    private Animator animator;
    private GameManager gameManager;
    private EnergyBar energy;
    private Movement movement;
    private IUnityService unityService;

    private float movementSpeed = 10;
    private float xMovementSpeed = 5f;
    private float xClamp = 4.8f;
    private bool grounded = true;
    private bool hasEnergy = true;
    
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        energy = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        if(unityService == null)
        {
            unityService = new UnityService();   
        }
    }

    /*
        Process movement while the player has energy.
        Game ends when the player has no energy.
    */
    void Update()
    {
        if(hasEnergy)
        {
            //ProcessMovement();
            if(movement != null)
            {
                var h = unityService.GetAxis("Horizontal");
                transform.Translate(movement.ProcessMovement(h, unityService.GetDeltaTime()));
                transform.localRotation = movement.ProcessRotation(h);
                ProcessJump();
            }
        }
        else
        {
            animator.SetFloat ("Speed", 0);							
        }
    }

    /*
        This method is used to allow the player to remain static until start button is selected.
    */
    public void SetupMovement()
    {
        movement = new Movement(movementSpeed, xMovementSpeed);
        animator.speed = 1.2f;								
        animator.SetFloat ("Speed", movementSpeed);	
    }

    /*
        Used for clamping the player the the screen, 
            https://community.gamedev.tv/t/mathf-clamp-doesnt-quite-clamp-correctly/27694
    */
    void FixedUpdate()
    {
        StartCoroutine(WaitForFixed());
    }

    /*
        Determines what happens when the player triggers a collider.
    */
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpawnTrigger")
        {
            // Spawns plots along the path.
            spawnManager.SpawnTriggerEntered();
        }
        if (other.tag == "Obstacle")
        {
            // Nothing needs to happen, collision with an obstacle and the slowing of the player due to this is sufficient punishment when combined with the energy system.
        }
        if(other.tag == "Ground")
        {
            // Player can jump only when grounded.
            grounded = true;
        }
        if(other.tag == "EnergyPickup")
        {
            // Increases the players energy.
			energy.GainPowerup();
        }
    }

    /*
        The player controlls movement along the x-axis,
    */

    IEnumerator WaitForFixed()
    {
        yield return new WaitForFixedUpdate();
        transform.position = HorizontalClamp();
    }


    private Vector3 HorizontalClamp()
    {
        return new Vector3(Mathf.Clamp(transform.position.x, -xClamp, xClamp),
                                    transform.position.y, transform.position.z);
    }

    /*
        Player can jump if grounded and thespace key is pressed.
        Jumping will start the jump animation.
    */
    private void ProcessJump(){
        if(Input.GetKeyDown("space")){
            if(grounded == true){
                grounded = false;
                rb.AddForce(movement.ProcessJump(), ForceMode.Impulse);
                animator.Play("Jump");
            }
        }
    }

}
