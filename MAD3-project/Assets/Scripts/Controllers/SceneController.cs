using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utilities;

public class SceneController : MonoBehaviour
{
    // Callback info: https://www.youtube.com/watch?v=3I5d2rUJ0pE

    private static Action onLoaderCallback;

    public void PlayGame()
    {
        // Set the loader callback action to load the game scene
        onLoaderCallback = () => {
            SceneManager.LoadSceneAsync(SceneNames.GAMESCENE);
        };

        SceneManager.LoadSceneAsync(SceneNames.LOADSCENE);
    }

    public void ExitGame()
    {
        // Set the loader callback action to load the game scene
        onLoaderCallback = () => {
            SceneManager.LoadSceneAsync(SceneNames.MENUSCENE);
        };

        SceneManager.LoadSceneAsync(SceneNames.LOADSCENE);
    }

    public static void LoaderCallback()
    {
        // Triggered after the first Update which lewts the scene refresh
        // Execute the loader callback action which will load the game scene
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    public static void MenuScene()
    {
        SceneManager.LoadSceneAsync(SceneNames.MENUSCENE);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
