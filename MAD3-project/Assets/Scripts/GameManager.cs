using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*
        GameManager updates the distance travelled and updates it to the screen.
    */
    
    private SceneController sceneController;
    private Transform maincamera;
    [SerializeField] private Text distanceTravelled;
    private int offset = 2;
    private int dist;

    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
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
       dist = Mathf.RoundToInt(maincamera.position.z) + offset;
       distanceTravelled.text = dist.ToString() + "m";
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("DistanceTravelled", dist);
        sceneController.Gameover();
    }
}
