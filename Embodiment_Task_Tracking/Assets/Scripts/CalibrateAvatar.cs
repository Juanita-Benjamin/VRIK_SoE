using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CalibrateAvatar : MonoBehaviourPun
{
    public Transform hmd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Calibrate()
    {
        if (!PhotonNetwork.IsMasterClient && photonView.IsMine)
        {
            // Tell the host to calibrate using this HMD position
            photonView.RPC("CalibrateHMD", RpcTarget.MasterClient, hmd.position);
            Debug.Log("[Calibration] Sent HMD position to Host");
        }
    }
}
