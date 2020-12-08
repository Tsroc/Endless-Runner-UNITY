using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchMenu : MonoBehaviour
{
    /*
        LaunchMenu controls the launch scene.
        The launch scene will transition into the Menuscene once the return key is pressed.
    */

    private SceneController sceneController;

    private void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }
    
    /*
        The launch scene will transition to the menu scene when the Return key is pressed.
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LaunchGame();
        }       
    }

    private void LaunchGame()
    {
        sceneController.MenuScene();
    }
}
