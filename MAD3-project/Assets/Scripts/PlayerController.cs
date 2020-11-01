using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float xMovementSpeed = 5.0f;
    private SpawnManager spawnManager;
    private float yaw = 20.0f;
    private float screenClamp = 4.8f;
    // Start is called before the first frame update
    void Start()
    {
       spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update() {
        ProcessMovement();
        ProcessRotation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Used for clamping the player: https://community.gamedev.tv/t/mathf-clamp-doesnt-quite-clamp-correctly/27694
        StartCoroutine(WaitForFixed());
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }

    //private void processMovement()
    private void ProcessMovement()
    {
        float hMovement = Input.GetAxis("Horizontal") * xMovementSpeed;
        float vMovement =  movementSpeed; // /2

        transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
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

    private void ProcessRotation()
    {
        if (Input.GetAxis("Horizontal") > 0)    // Moves Right.
            transform.localRotation = Quaternion.Euler(0, yaw, 0);
        else if (Input.GetAxis("Horizontal") < 0)   // Moves Left.
            transform.localRotation = Quaternion.Euler(0, -yaw, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
