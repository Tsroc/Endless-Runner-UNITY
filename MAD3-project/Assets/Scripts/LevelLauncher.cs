using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private float levelStartDelay = 1.5f;
    [SerializeField] private GameObject startButton;
    GameObject player = null;
    private EnergyBar energy;

    private int currentPlayer = 0;

    void Start()
    {

        Vector3 startPosition = new Vector3(0, 0, 0);
        Quaternion startRotation = new Quaternion(0, 0, 0, 0);

        startButton.SetActive(false);

        if(PhotonNetwork.IsConnected)
        {
            if (NetworkedPlayer.LocalPlayerInstance == null)  // Instantiate the network prefabs.
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    startPosition += new Vector3(-2, 0, 0);
                    player = PhotonNetwork.Instantiate(playerPrefabs[0].name,
                                                                startPosition, startRotation, 0);
                } else {
                    startPosition += new Vector3(2, 0, 0);
                    player = PhotonNetwork.Instantiate(playerPrefabs[0].name,
                                                                startPosition, startRotation, 0);
                }
            }
            
            player.gameObject.tag="Player"; 

            if(PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
        }
        else    // Single player game
        {
            player = Instantiate(playerPrefabs[0]);
            startPosition += new Vector3(-2, 0, 0);
            player.transform.position = startPosition;
            player.transform.rotation = startRotation;
            // Adjust view to put ship in view of camera
            player.SetActive(true);
            Invoke("StartTheLevel", levelStartDelay);

        }
        
        player.GetComponent<PlayerController>().enabled = true;
    }

    [PunRPC]
    private void StartTheLevel()
    {
        energy = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
        player.SendMessage("SetupMovement");
        energy.SendMessage("SetEnergyLevel", 1);
    }

    public void StartMultiPlayerGame()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("StartTheLevel", RpcTarget.All); 
        startButton.SetActive(false);

    }
}