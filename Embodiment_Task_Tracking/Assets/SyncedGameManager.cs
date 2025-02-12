using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class SyncedGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] bool isHosting = true;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
            
        }
        else
        {
            Debug.Log("Trying to connect");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions());

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to main server");

        if (isHosting)
        {
            Debug.Log("Creating room");
            CreateRoom();
        }
        else
        {
            Debug.Log("Joining random room...");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Disconnected due to {cause}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JOINED ROOM");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"FAILED JOIN ROOM");
    }
}
