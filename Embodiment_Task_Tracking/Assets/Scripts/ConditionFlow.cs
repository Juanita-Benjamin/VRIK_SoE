using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
public class ConditionFlow : MonoBehaviour
{
    //get a list of the avatars
    public List<GameObject> avatars;
    //public GameObject[] avatar;
    [SerializeField] GameObject lowerBody;
    public GameObject upperBody;

    [Header("Avatar Prefabs")]
    [SerializeField] Dictionary<string, GameObject> avatarPrefabs;

    //UI Components
    public TMP_InputField PID;
    public TMP_Dropdown cohort_dropdown, gender_dropdown, race_dropdown;
    public bool isHandstracking, isController, isTorso, isFullbody;

    // Start is called before the first frame update
    void Start()
    {
        //Use dictionary to store the avatar information
        
        //Note: this dictionary will be used to activate the avatars that are 
        //inactive

        avatarPrefabs = new Dictionary<string, GameObject>()
        {
            {"Black Female", avatars[0] },
            {"Black Male", avatars[1] },
            {"Asian Female", avatars[2] },
            {"Asian Male", avatars[3] },
            {"White Female", avatars[4] },
            {"White Male", avatars[5] },
            {"Hispanic Female", avatars[6] },
            {"Hispanic Male", avatars[7] }
        };

        //get the lower half of the avatar
        if (upperBody.activeSelf)
        {
            lowerBody = GameObject.Find("Lower Body");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                lowerBody.SetActive(false);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Task_And_Avatar()
    {
        //switch ()
        //{

        //}
    }

    public void InstantiateAvatar()
    {

    }

    public void ParticipantData()
    {
        string dateTime = DateTime.Now.ToString("MM-dd-HH-mm");
        string ParticipantID = PID.GetComponent<TMP_InputField>().text;
    }
}
