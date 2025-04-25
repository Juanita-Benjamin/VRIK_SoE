using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrate : MonoBehaviour
{
    [Header("Targets")]
    public Transform headTarget;
    public Transform hipTracker;
    public Transform leftFootTracker;
    public Transform rightFootTracker;

    [Header("Offsets")]
    [SerializeField] Vector3 initialHipOffset;
    [SerializeField] Vector3 initialRightFootOffset;
    [SerializeField] Vector3 initialLeftFootOffset;

    // Start is called before the first frame update
    void Start()
    {
        initialHipOffset = hipTracker.position - headTarget.position;
        initialLeftFootOffset = leftFootTracker.position - headTarget.position;
        initialRightFootOffset = rightFootTracker.position - headTarget.position;   
    }

    // Update is called once per frame
    void Update()
    {

        CalibrateAvatar();
        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space))
        {
            

        }
    }

    public void CalibrateAvatar()
    {
        hipTracker.position = headTarget.position + initialHipOffset;
        leftFootTracker.position = headTarget.position + initialLeftFootOffset;
        rightFootTracker.position = headTarget.position + initialRightFootOffset;
    }
}
