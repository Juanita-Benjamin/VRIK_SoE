using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class SyncedGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] bool isHosting = true;
    int count = 0;
  

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
            Debug.Log("Connected");
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
        Debug.Assert(PhotonNetwork.IsConnected);
        Debug.Log("I have joined" + count);
        count++;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"FAILED JOIN ROOM");
    }
}
