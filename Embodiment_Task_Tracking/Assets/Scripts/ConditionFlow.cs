using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


public class ConditionFlow : MonoBehaviour
{
    //enums
    public enum Cohorts
    {
        One, Two, Three, Four, Five, Six, Seven, Eight
    };

    public Cohorts currentCohort;

    //get a list of the avatars
    public List<GameObject> avatars;

    //list of elements
    public GameObject tableTask, floorTask, lowerBody;

    //Pause and end scree
    public GameObject pauseScreen, endScreen;

    [Header("Avatar Prefabs")]
    [SerializeField] private Dictionary<string, GameObject> avatarPrefabs;

    //UI Components
    public TMP_InputField PID;

    public TMP_Dropdown cohort_dropdown, gender_dropdown, race_dropdown;
    public bool isHandstracking, isFullbody, isFloorTask, isTableTask;
    public bool pause;

    private string log_path = "Participant_Log_Data.csv";

    //cohort track
    [SerializeField] private int cohortCount = 0; //cohort count: 8
    [SerializeField] private int trialCount = 0; //trial within the cohort: 8

    [SerializeField]private string[,] conditions = new string[8, 8]
    {
        {"HT + FullBody + Table Task", "HT + FullBody + Floor Task", "HT + Torso + Table task", "Controller + FullBody + Floor Task",  "HT + Torso +  Floor Task", "Controller + FullBody + Table Task","Controller + Torso + Floor Task", "Controller + Torso + Table Task"},
        {"HT + FullBody + Floor Task", "Controller + FullBody + Floor Task",  "HT + FullBody + Table Task",  "Controller + FullBody + Table Task",  "HT + Torso + Table task"," Controller + Torso + Table Task", "HT + Torso +  Floor Task", "Controller + Torso + Floor Task"},
        {"Controller + FullBody + Floor Task", "Controller + FullBody + Table Task","HT + FullBody + Floor Task","Controller + Torso + Table Task", "HT + FullBody + Table Task", "Controller + Torso + Floor Task","HT + Torso + Table task","HT + Torso +  Floor Task"},
        {"Controller + FullBody + Table Task","Controller + Torso + Table Task", "Controller + FullBody + Floor Task",  "Controller + Torso + Floor Task", "HT + FullBody + Floor Task",  "HT + Torso +  Floor Task", "HT + FullBody + Table Task", "HT + Torso + Table task" },
        {"Controller + Torso + Table Task",  "Controller + Torso + Floor Task", "Controller + FullBody + Table Task",  "HT + Torso +  Floor Task", "Controller + FullBody + Floor Task",  "HT + Torso + Table task", "HT + FullBody + Floor Task", " HT + FullBody + Table Task"},
        {"Controller + Torso + Floor Task", "HT + Torso +  Floor Task",    "Controller + Torso + Table Task", "HT + Torso + Table task"," Controller + FullBody + Table Task",  "HT + FullBody + Table Task", " Controller + FullBody + Floor Task",  "HT + FullBody + Floor Task"},
        {"HT + Torso +  Floor Task", " HT + Torso + Table task", "Controller + Torso + Floor Task"," HT + FullBody + Table Task",  "Controller + Torso + Table Task", "HT + FullBody + Floor Task", " Controller + FullBody + Table Task",  "Controller + FullBody + Floor Task"},
        {"HT + Torso + Table task", "HT + FullBody + Table Task",  "HT + Torso +  Floor Task", "HT + FullBody + Floor Task",  "Controller + Torso + Floor Task", "Controller + FullBody + Floor Task",  "Controller + Torso + Table Task", "Controller + FullBody + Table Task"}
    };

    // Start is called before the first frame update
    private void Start()
    {
        //Use dictionary to store the avatar information
        //Note: this dictionary will be used to activate the avatars that are inactive

        avatarPrefabs = new Dictionary<string, GameObject>()
        {
            {"Black_Female", avatars[0] },
            {"Black_Male", avatars[1] },
            {"Asian_Female", avatars[2] },
            {"Asian_Male", avatars[3] },
            {"White_Female", avatars[4] },
            {"White_Male", avatars[5] },
            {"Hispanic_Female", avatars[6] },
            {"Hispanic_Male", avatars[7] }
        };
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    public void InstantiateAvatar()
    {

        string selectedRace = race_dropdown.options[race_dropdown.value].text;
        string selectedGender = gender_dropdown.options[gender_dropdown.value].text;

        string key = $"{selectedRace}_{selectedGender}";

        if (avatarPrefabs.ContainsKey(key))
        {
            Debug.Log("Key found");
            avatarPrefabs[key].SetActive(true);
        }
        else
        {
            Debug.Log("Key not found");
        }

        foreach (var avatar in avatars)
        {
            if (avatar.activeSelf)
            {
                Debug.Log(avatar.name);
                lowerBody = GameObject.Find($"{avatar.name}/Lower Body"); 
            }
        }
    }
    public void NextTrial()
    {
        //update the trial count and then
        pauseScreen.SetActive(false);
        trialCount++;
        if (trialCount >= 8)
        {
            trialCount = 0;
            endScreen.SetActive(true);
            tableTask.SetActive(false);
            floorTask.SetActive(false);
        }
        updateCondition();
    }
    public void StartConditions()
    {
        UpdateCohortDropdown();
    }
    public void UpdateCohortDropdown()
    {
        currentCohort = (Cohorts)cohort_dropdown.value;
        trialCount = 0;
    }

    void updateCondition()
    {
        

        cohortCount = (int)currentCohort;
        int trialIndex = trialCount;
        string currentCondition = conditions[cohortCount, trialIndex];

        Debug.Log($"Cohort {(cohortCount + 1)}, Trial {trialCount + 1}: {currentCondition}");

        isHandstracking = false;
        isFullbody = false;
        tableTask.SetActive(false);
        floorTask.SetActive(false);


        if (currentCondition.Contains("HT"))
        {
            isHandstracking = true;
        }
        if (currentCondition.Contains("FullBody"))
        {
            isFullbody = true;
            lowerBody.gameObject.SetActive(true);
        }
        else
        {
            lowerBody.gameObject.SetActive(false);
        }

        if (currentCondition.Contains("Table Task"))
        {
            tableTask.SetActive(true);
            isTableTask = true;
        }
        if (currentCondition.Contains("Floor Task")) { floorTask.SetActive(true); isFloorTask = true; }
    }


    public void ParticipantData()
    {
        string dateTime = DateTime.Now.ToString("MM-dd-HH-mm");
        string ParticipantID = PID.GetComponent<TMP_InputField>().text;
        bool fullBody = isFullbody;
        bool handTracking = isHandstracking;
        string avatar_gender = gender_dropdown.options[gender_dropdown.value].text;
        string avatar_race = race_dropdown.options[race_dropdown.value].text;
        string cohort = cohort_dropdown.options[cohort_dropdown.value].text;

        if (!File.Exists(log_path))
        {
            using StreamWriter writer = File.CreateText(log_path);
            writer.WriteLine("ParticipantID, Gender, Race, cohort #, handtracking, fullbody date-time");
            writer.WriteLine($"{ParticipantID}, {avatar_gender}, {avatar_race}, {cohort}, {handTracking},{fullBody} ,{dateTime}");
        }
        else
        {
            using StreamWriter writer = File.AppendText(log_path);
            writer.WriteLine($"{ParticipantID}, {avatar_gender}, {avatar_race}, {cohort},{handTracking},{fullBody},{dateTime}");
        }
    }
}