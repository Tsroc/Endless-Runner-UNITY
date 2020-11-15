using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchMenu : MonoBehaviour
{
    private SceneController sceneController;

    private void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }
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
