using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class LowerBodyNetwork : MonoBehaviour
{
    public Transform rightFootTracker, leftFootTracker, hipTracker;
    [SerializeField] private Vector3 rightFootPos, leftFootPos, hipPos;
    [SerializeField] private Quaternion rightFootRot, leftFootRot, hipRot;
    [SerializeField] private PhotonView[] photonView;
    //[SerializeField] private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < photonView.Length; i++)
        {
            photonView[i] = GetComponent<PhotonView>();
        }

        //photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!photonView.IsMine)
        //{
        //   Tracker.position = Vector3.Lerp(Tracker.position, TrackerPos, Time.deltaTime * 10);
        //   Tracker.rotation = Quaternion.Lerp(Tracker.rotation, TrackerRot, Mathf.Clamp01(Time.deltaTime * 10));
        //}
        for (int i = 0; i < photonView.Length; i++)
        {
            if (!photonView[i].IsMine)
            {
                hipTracker.position = Vector3.Lerp(hipTracker.position, hipPos, Time.deltaTime * 10);
                hipTracker.rotation = Quaternion.Lerp(hipTracker.rotation, hipRot, Mathf.Clamp01(Time.deltaTime * 10));

                leftFootTracker.position = Vector3.Lerp(leftFootTracker.position, leftFootPos, Time.deltaTime * 10);
                leftFootTracker.rotation = Quaternion.Lerp(leftFootTracker.rotation, leftFootRot, Time.deltaTime * 10);

                rightFootTracker.position = Vector3.Lerp(rightFootTracker.position, rightFootPos, Time.deltaTime * 10);
                rightFootTracker.rotation = Quaternion.Lerp(rightFootTracker.rotation, rightFootRot, Time.deltaTime * 10);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(Tracker.position);
            //stream.SendNext(Tracker.rotation); 

            stream.SendNext(hipTracker.position);
            stream.SendNext(hipTracker.rotation);

            stream.SendNext(leftFootTracker.position);
            stream.SendNext(leftFootTracker.rotation);

            stream.SendNext(rightFootTracker.position);
            stream.SendNext(rightFootTracker.rotation);
        }

        else
        {
            //TrackerPos = (Vector3)stream.ReceiveNext();
            //TrackerRot = (Quaternion)stream.ReceiveNext();

            hipPos = (Vector3)stream.ReceiveNext();
            hipRot = (Quaternion)stream.ReceiveNext();

            leftFootPos = (Vector3)stream.ReceiveNext();
            leftFootRot = (Quaternion)stream.ReceiveNext();

            rightFootPos = (Vector3)stream.ReceiveNext();
            rightFootRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
