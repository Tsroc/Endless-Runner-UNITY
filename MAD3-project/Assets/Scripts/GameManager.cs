using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
        GameManager updates the distance travelled and updates it to the screen.
    */
    
    private Transform camera;
    [SerializeField] private Text distanceTravelled;
    private int offset = 2;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        UpdateDistance();
    }
    
    /*
        Updates the distance based on movement along the z-axis.
    */
    private void UpdateDistance()
    {
       int dist = Mathf.RoundToInt(camera.position.z) + offset;
       distanceTravelled.text = dist.ToString() + "m";
    }
}
