using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LowerBodyReceiver : MonoBehaviourPun
{
    public Transform hipTarget, rightFootTarget, leftFootTarget;

    private Vector3 hipPos, rightFootPos, leftFootPos;
    private Quaternion hipRot, rightRot, leftRot;

    private PhotonView hip, left, right;
    
    // Start is called before the first frame update
    void Start()
    {
        hip = hipTarget.GetComponent<PhotonView>();
        left = leftFootTarget.GetComponent<PhotonView>();
        right = rightFootTarget.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hip.IsMine) {

            hipTarget.position = Vector3.Lerp(hipTarget.position, hipPos, Time.deltaTime * 10);
            hipTarget.rotation = Quaternion.Slerp(hipTarget.rotation, hipRot, Time.deltaTime * 10);

            leftFootTarget.position = Vector3.Lerp(leftFootTarget.position, leftFootPos, Time.deltaTime * 10);
            leftFootTarget.rotation = Quaternion.Slerp(leftFootTarget.rotation, leftRot, Time.deltaTime * 10);

            rightFootTarget.position = Vector3.Lerp(rightFootTarget.position, rightFootPos, Time.deltaTime * 10);
            rightFootTarget.rotation = Quaternion.Slerp(rightFootTarget.rotation, rightRot, Time.deltaTime * 10);

        }
    }

    public void OnPhotonSerializedView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
        }
        else
        {
            hipPos = (Vector3)stream.ReceiveNext();
            hipRot = (Quaternion)stream.ReceiveNext();

            leftFootPos = (Vector3)stream.ReceiveNext();
            leftRot = (Quaternion)stream.ReceiveNext();

            rightFootPos = (Vector3)stream.ReceiveNext();
            rightRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
