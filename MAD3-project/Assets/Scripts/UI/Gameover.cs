using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [SerializeField] private Text distanceTravelled;

    private void Start()
    {
        UpdateDistance();
    }

    /*
        Updates the gameover distance travelled.
    */
    private void UpdateDistance()
    {
        int dist = PlayerPrefs.GetInt("DistanceTravelled");
        distanceTravelled.text = "You Ran \n";
        distanceTravelled.text += dist.ToString() + " m";
    }

}
