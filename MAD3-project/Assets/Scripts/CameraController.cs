using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
        CameraController controls the game camera.
    */

    private Transform player;
    private float yOffset = 2;
    private float zOffset = -2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /*
        The camera is positioned behind, and follows the player.
    */
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffset);
    }
}
