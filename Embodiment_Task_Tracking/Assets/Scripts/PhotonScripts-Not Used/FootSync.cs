//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using Oculus.Interaction.PoseDetection;

//public class FootSync : MonoBehaviourPun
//{
//    [Header("SetUp")]
//    public Transform leftFootCube;

//    public Transform rightFootCube;
//    public Transform headCube;

//    public Transform trackerLeft;
//    public Transform trackerRight;
//    public Transform head;

//    private Vector3 leftFootOffset;
//    private Vector3 rightFootOffset;
//    private Vector3 headOffset;

//    //private float lastSentTime = 0;
//    //private float sendEverySeconds = 1;

//    private PhotonView photonView;

//    // Start is called before the first frame update
//    private void Start()
//    {
//        photonView = head.GetComponent<PhotonView>();
//    }

//    // Update is called once per frame
//    private void Update()
//    {
//        //if (PhotonNetwork.IsMasterClient)
//        //{
//        //    // Sender: calculate offset relative to Virtual Head
//        //    if (trackerLeft && head)
//        //        leftFootOffset = trackerLeft.position - head.position;

//        //    if (trackerRight && head)
//        //        rightFootOffset = trackerRight.position - head.position;
//        //}

//        //photonView.RPC("SyncOffsets", RpcTarget.Others, leftFootOffset, rightFootOffset);

//        //else
//        //{
//        //    // Receiver: apply localPosition to cubes relative to HMD
//        //    if (leftFootCube)
//        //        leftFootCube.localPosition = leftFootOffset;

//        //    if (rightFootCube)
//        //        rightFootCube.localPosition = rightFootOffset;
//        //}

//        //if (Input.GetKeyUp(KeyCode.Space))
//        //{
//            //CalibrateLocally();
//            //print("SENDING.");
//            //photonView.RPC("CalibrateHMD", RpcTarget.Others, headCube.transform.position, headCube.transform.rotation);
//            //CalibrateHMD(headCube.transform.position);
//        //}
//    }

//    //public void OnPhotonSerializedView(PhotonStream stream, PhotonMessageInfo info)
//    //{
//    //    if (stream.IsWriting)
//    //    {
//    //        stream.SendNext(leftFootOffset);
//    //        stream.SendNext(rightFootOffset);
//    //    }

//    //    else
//    //    {
//    //        // Receive from network
//    //        leftFootOffset = (Vector3)stream.ReceiveNext();
//    //        rightFootOffset = (Vector3)stream.ReceiveNext();
//    //    }
//    //}

//    [PunRPC]
//    private void SyncOffsets(Vector3 leftOffset, Vector3 rightOffset)
//    {
//        leftFootOffset = leftOffset;
//        rightFootOffset = rightOffset;

//        if (leftFootCube)
//        {
//            leftFootCube.localPosition = leftOffset;
//        }

//        if (rightFootCube)
//        {
//            rightFootCube.localPosition = rightOffset;
//        }
//    }

//    [PunRPC]
//    private void CalibrateHMD(Vector3 clientHMDPosition)
//    {
//        Debug.Log($"clientHmdPos {clientHMDPosition}");

//        if (!PhotonNetwork.IsMasterClient || head == null) return;

//        // Host compares Meta PC's HMD to its own virtual head
//        headOffset = clientHMDPosition - head.position;
//        Debug.Log("[Calibration] HMD Offset set to: " + headOffset);
//        transform.parent.position += headOffset;
//    }

//    //private void FixedUpdate()
//    //{
//    //    return;
//    //    if (!PhotonNetwork.IsMasterClient && headCube != null)
//    //    {
//    //        //send every 1sec
//    //        if (Time.time - lastSentTime > sendEverySeconds)
//    //        {
//    //            Debug.Log("Sent just now!");
//    //            photonView.RPC("CalibrateHMD", RpcTarget.Others, headCube.transform.position, headCube.transform.eulerAngles.y);
//    //            lastSentTime = Time.time;
//    //        }
//    //    }
//    //}
//    //private void CalibrateLocally()
//    //{
//        //transform.position = 
//    //}
//}