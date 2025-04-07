using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTrackingSpace : MonoBehaviour
{
    [SerializeField] private Transform viveTrackingSpace, viveHmd, questHmd;

    private void Update()
    {
        // Get space button press

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SyncSpace();
        }
    }

    private void SyncSpace()
    {
        // Compute the offset between ViveHMD and QuestHMD
        // Offset the HMDTrackingSpace by that offset


        // rotate around Y axis only
        var yRotOffset = questHmd.eulerAngles.y - viveHmd.eulerAngles.y;
        viveTrackingSpace.rotation = Quaternion.Euler(0, yRotOffset, 0);

        var offset = questHmd.position - viveHmd.position;
        viveTrackingSpace.position += offset;

        //offset = questHmd.position - viveHmd.position;
        //viveTrackingSpace.position += offset;
    }
}