using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utilities;

public class SceneController : MonoBehaviour
{
    /*
        SceneController manages transitions between scenes.
    */

    // Allows for the loadscene to be shown while the scene assigned to this Action loads.
    // Callback info: https://www.youtube.com/watch?v=3I5d2rUJ0pE
    private static Action onLoaderCallback;


    /*
        Will call the gamescene, loadscene will load on the next update and remain until the gamescene is loaded.
    */
    public void PlayGame()
    {
        // Set the loader callback action to load the game scene
        onLoaderCallback = () => {
            SceneManager.LoadSceneAsync(SceneNames.GAMESCENE);
        };

        SceneManager.LoadSceneAsync(SceneNames.LOADSCENE);
    }

    /*
        Will call the menuscene, loadscene will load on the next update and remain until the menuscene is loaded.
    */
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
        // Triggered after the first Update which lets the scene refresh
        // Execute the loader callback action which will load the game scene
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    /*
        Loads the gameoverscene additively.
    */
    public void Gameover()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAMEOVERSCENE, LoadSceneMode.Additive);
    }

    /*
        Loads the menuscene.
    */
    public void MenuScene()
    {
        SceneManager.LoadSceneAsync(SceneNames.MENUSCENE);
    }

    /*
        Exits the application.
    */
    public void QuitGame(){
        Application.Quit();
    }
}
