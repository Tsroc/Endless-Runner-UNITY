using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
        GameManager updates the distance travelled and updates it to the screen.
    */
    
    private GameObject player;
    [SerializeField] private Text distanceTravelled;

    void Start()
    {
        player = GameObject.Find("Player");
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
       int dist = Mathf.RoundToInt(player.transform.position.z);
       distanceTravelled.text = dist.ToString() + "m";
    }
}
