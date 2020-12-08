﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
        PlayerController manages the controls available to the player, along with collision management.
    */

    [SerializeField] private float jumpVelocity = 5.0f; 
    
    private SpawnManager spawnManager;
    private Rigidbody rb;
    private Animator animator;
    private GameManager gameManager;
    private EnergyBar energy;

    private float movementSpeed = 0;//10f;
    private float xMovementSpeed = 0f;
    private float yaw = 15.0f;
    private float screenClamp = 4.8f;
    private bool grounded = true;
    private bool hasEnergy = true;
    
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        energy = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();

        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

    }

    /*
        Process movement while the player has energy.
        Game ends when the player has no energy.
    */
    void Update()
    {
        if(hasEnergy)
        {
            ProcessMovement();
            ProcessJump();
            ProcessRotation();
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
        movementSpeed = 10f;
        xMovementSpeed = 5f;
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
            Debug.Log("Obstacle collision");
        }
        if(other.tag == "Ground")
        {
            // Player can jump only when grounded.
            grounded = true;
        }
        if(other.tag == "EnergyPickup")
        {
            // Increases the players energy.
            Debug.Log("EnergyPickup");
			energy.GainPowerup();
        }

    }

    /*
        The player controlls movement along the x-axis,
    */
    private void ProcessMovement()
    {
        float hMovement = Input.GetAxis("Horizontal") * xMovementSpeed;
        transform.Translate(new Vector3(hMovement, 0, movementSpeed) * Time.deltaTime);
    }

    IEnumerator WaitForFixed()
    {
        yield return new WaitForFixedUpdate();
        transform.position = ClampingMethod();
    }

    /*
        Clamps the player within a specified screen area.
    */
    private Vector3 ClampingMethod()
    {
        return new Vector3(Mathf.Clamp(transform.position.x, -screenClamp, screenClamp), transform.position.y, transform.position.z);
    }

    /*
        Player can jump if grounded and thespace key is pressed.
        Jumping will start the jump animation.
    */
    private void ProcessJump(){
        if(Input.GetKeyDown("space")){
            if(grounded == true){
                grounded = false;
                rb.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.Impulse);
                animator.Play("Jump");
            }
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetAxis("Horizontal") > 0 && movementSpeed > 0)
        {   
            // Moves Right.
            transform.localRotation = Quaternion.Euler(0, yaw, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0 && movementSpeed > 0) 
        {

            // Moves Left.
            transform.localRotation = Quaternion.Euler(0, -yaw, 0);
        }
        else
        {
            // Resets rotation if not moving along the x-axis.
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
