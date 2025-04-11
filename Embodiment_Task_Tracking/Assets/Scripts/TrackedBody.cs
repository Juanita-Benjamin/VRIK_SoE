using Oculus.Platform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackedBody : MonoBehaviour
{
    public ConditionFlow ConditionFlow;


    public List<GameObject> TrackedBodies;

    public GameObject ParticipantID;

    string PID;

    private StreamWriter writer;
    private StringBuilder stringBuilder;
    private string log_path;
    public bool startTracking;
    private float writingTime = 60.0f;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        stringBuilder = new StringBuilder();
    }


    public void StartTrackingOn()
    {
        startTracking = true;

        if (startTracking)
        {
            //get current cohoort
            var cohort = ConditionFlow.currentCohort;
            PID = ParticipantID.GetComponent<TMP_InputField>().text;
            log_path = $"Tracking/{"Cohort "}+{cohort}/{PID}_{DateTime.Now.ToString("MMddyy-HHmm")}.csv";
            if (!Directory.Exists(log_path))
            {
                //may need to do: Directory.CreateDirectory($"Tracking/{Cohort }+{cohort}");
                Directory.CreateDirectory(log_path);
            }

            string headers = "Timestamp, TimeElapsed,"; //placeholder for now
            for (int i = 0; i < TrackedBodies.Count; i++)
            {
                string bodyparts = TrackedBodies[i].name;
                headers += $"{bodyparts}_Pos(x),{bodyparts}_Pos(y), {bodyparts}_Pos(z),{bodyparts}_Quat_(x),{bodyparts}_Quat_(y),{bodyparts}_Quat_(z),{bodyparts}_Quat_(w),{bodyparts}_Six_(x),{bodyparts}_Six_(y),{bodyparts}_Six_(z),{bodyparts}_Six_(a),{bodyparts}_Six_(b),{bodyparts}_Six_(c),";

            }

            //this will add headers to the csv file
            writer = new StreamWriter(log_path);
            writer.Write(headers);
        }

        Debug.Log("Tracking Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (startTracking) //may need to change to ConditionFlow bool
        {
            timeElapsed += Time.deltaTime; //increase time every second
            stringBuilder.Append($"\n{DateTime.Now.ToUniversalTime()}, {timeElapsed:0.0000},");

            for (int i = 0; i < TrackedBodies.Count; i++)
            {
                Quaternion quats = TrackedBodies[i].transform.rotation;
                float[] sixDegrees = SixDConversions.To6D(quats); //convert the Quaternion values to 6DoF
                stringBuilder.Append($"{TrackedBodies[i].transform.position.x:0.0000},{TrackedBodies[i].transform.position.y:0.0000},{TrackedBodies[i].transform.position.z:0.0000},{TrackedBodies[i].transform.rotation.x:0.0000}, {TrackedBodies[i].transform.rotation.y:0.0000}, {TrackedBodies[i].transform.rotation.z:0.0000},{TrackedBodies[i].transform.rotation.w:0.0000},");
                stringBuilder.Append($"{sixDegrees[0]:0.0000}, {sixDegrees[1]:0.0000},{sixDegrees[2]:0.0000},{sixDegrees[3]:0.0000}, {sixDegrees[4]:0.0000}, {sixDegrees[5]:0.0000},");
            }

        }
        writingTime -= Time.deltaTime;
        if (writingTime <= 0.0f)
        {
            writer.Write(stringBuilder.ToString());
            stringBuilder.Clear();
            writingTime = 60.0f;
        }

    }
    //MAY NOT NEED
    public void StopLoggingOff()
    {
        //turn the logging off
        startTracking = false;
        Debug.Log("tracking stopped");
        writer.Close();
    }
}
