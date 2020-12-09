using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public float zSpeed;
    public float xSpeed;
    private float yaw;
    private float jumpVelocity;

    public Movement(float z, float x)
    {
        zSpeed = z;
        xSpeed = x;
        yaw = 15.0f;
        jumpVelocity = 5.0f;
    }


    public Vector3 ProcessMovement(float h, float deltaTime)
    {
        var x = h * xSpeed;
        var z = zSpeed;

        return new Vector3(x, 0, z) * deltaTime;
    }

    public Quaternion ProcessRotation(float h)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        if(zSpeed > 0)
        {
            if(h > 0)
            {
                rotation = Quaternion.Euler(0, yaw, 0);
            }
            else if(h < 0)
            {
                rotation = Quaternion.Euler(0, -yaw, 0);
            }
        }
        return rotation;
    }

    public Vector3 ProcessJump()
    {
        return new Vector3(0, jumpVelocity, 0);
    }

}