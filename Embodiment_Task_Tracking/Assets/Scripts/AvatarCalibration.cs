using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using RootMotion.FinalIK;
using OVR.Input;

public class AvatarCalibration : MonoBehaviour
{
    [Tooltip("Root of Avatar")] public GameObject vrRig;
    [Tooltip("VRIK Component")] public VRIK VRIK;

    public Transform hipTarget, headTarget;
    [SerializeField] Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        //calculate the initial offset
        initialOffset = hipTarget.position - headTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Calibrate();
        }
    }

    public void Calibrate()
    {
        //adjust the hip position to match the current head position
        hipTarget.position = headTarget.position +initialOffset;
    }
    #region Calibraton


    [Header("Head")]
    public Transform OVRCameraRig;

    [Header("Data for Calibration")]
    public VRIKCalibrator.CalibrationData data = new VRIKCalibrator.CalibrationData();

    public bool calibrating = true;
    private void CalibrateAvatar()
    {
        //data = VRIKCalibrator.Calibrate(VRIK, );
    }

    #endregion

    
}
