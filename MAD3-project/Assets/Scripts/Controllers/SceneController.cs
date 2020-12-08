using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Realtime;
using Photon.Pun;

using Utilities;

public class SceneController : MonoBehaviourPunCallbacks
{
    /*
        SceneController manages transitions between scenes.
    */

    // Allows for the loadscene to be shown while the scene assigned to this Action loads.
    // Callback info: https://www.youtube.com/watch?v=3I5d2rUJ0pE
    private static Action onLoaderCallback;
    [SerializeField] Text feedbackText;

    // Network variables
    byte maxPlayersPerRoom = 2;
    bool IsConnecting;
    private string gameVersion = "1";

    void Awake()
    {
        // Join a network.
        PhotonNetwork.AutomaticallySyncScene = true;        
    }

    /*
        Will call the gamescene, loadscene will load on the next update and remain until the gamescene is loaded.
    */
    public void ConnectSinglePlayer()
    {
        // Set the loader callback action to load the game scene
        onLoaderCallback = () => {
            SceneManager.LoadSceneAsync(SceneNames.GAMESCENE);
        };

        SceneManager.LoadSceneAsync(SceneNames.LOADSCENE);
    }

 public void ConnectMultiPlayer()
    {
        feedbackText.text = "";
        IsConnecting = true;
        //PhotonNetwork.NickName = playerName.text;

        if(PhotonNetwork.IsConnected)
        {
            feedbackText.text += "\nJoining a Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            feedbackText.text += "\nConnecting to Network...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Add Photon callbacks
    public override void OnConnectedToMaster()
    {
        if(IsConnecting)
        {
            feedbackText.text += "\nConnected, joining room...";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        feedbackText.text += "\nFailed to join random room...";
        // If I'm the first player, create new room
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        feedbackText.text += "\nDisconnected: " + cause + "...";
        IsConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        feedbackText.text += "\nSuccessfully joined room, game starting...";
        feedbackText.text += "\nThere are " + PhotonNetwork.CurrentRoom.PlayerCount + " other players...";
        PhotonNetwork.LoadLevel(SceneNames.GAMESCENE);

        /*
        onLoaderCallback = () => {
            PhotonNetwork.LoadLevel(SceneNames.GAMESCENE);
        };

        PhotonNetwork.LoadLevel (SceneNames.LOADSCENE);
        */
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
        SceneManager.LoadSceneAsync(SceneNames.GAMEOVERSCENE);
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
