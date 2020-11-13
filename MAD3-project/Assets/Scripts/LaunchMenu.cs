using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LaunchGame();
        }       
    }

    private void LaunchGame()
    {
        SceneController.MenuScene();
    }
}
