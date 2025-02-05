using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class SyncedGameManager : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        Debug.Log("Trying to connect");

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Joining random room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Trying to connect");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("connected to main server");

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Disconnected due to {cause}");
    }
}
