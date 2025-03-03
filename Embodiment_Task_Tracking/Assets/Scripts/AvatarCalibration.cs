using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using RootMotion.FinalIK;
using OVR.Input;

public class AvatarCalibration : MonoBehaviour
{
    //[Tooltip("Root of Avatar")] public GameObject vrRig;
    [Header("VRIK Component")] public VRIK vrik;

    [Header("VR Tracking References")]
    public Transform hipTarget, headTarget;
    public Transform leftHandAnchor, rightHandAnchor;

    [Header("Calibration Offsets")]
    public Vector3 headOffset;
    public Vector3 handOffset;
    public float scaleMultiplier = 1.0f;
    public bool fixedscale = true;

    private VRIKCalibrator.CalibrationData calibrationData;

    // Start is called before the first frame update
    void Start()
    {
        //calculate the initial offset
        //initialOffset = hipTarget.position - headTarget.position;
        Debug.Log("Starting VRIK Calibration");
        Calibrate();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.Space))
        {
            Calibrate();
            
        }
    }
    #region Calibraton


    public void Calibrate()
    {
        //adjust the hip position to match the current head position
        //hipTarget.position = headTarget.position +initialOffset;

        if (vrik == null)
        {
            Debug.Log("VRIK Component is missing");
            return;
        }

        //conduct VRIK Calibration
        calibrationData = VRIKCalibrator.Calibrate(
            vrik,
            headTarget,
            leftHandAnchor,
            rightHandAnchor,
            handOffset,
            Vector3.zero, //No extra head rotation offset
            handOffset,
            Vector3.zero, //No extra hand rotation offset
            scaleMultiplier
            );


        if (fixedscale)
        {
            vrik.references.root.localScale = scaleMultiplier * Vector3.one;
            calibrationData.scale = scaleMultiplier;
        }

        //assign pelvis tracking for better lower body movemebt
        if (hipTarget != null)
        {
            vrik.solver.spine.pelvisTarget = hipTarget;
            vrik.solver.spine.pelvisPositionWeight = 1.0f;
            vrik.solver.spine.pelvisRotationWeight = 1.0f;
            vrik.solver.spine.maintainPelvisPosition = 1.0f; //stabilize lower body movement

        }

        else 
        {
            Debug.LogWarning("Lower body may not be tracking");
        
        }
    }

    #endregion


}
