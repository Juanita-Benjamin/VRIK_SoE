using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementReset : MonoBehaviour
{
    [SerializeField] private SnapPiece[] snapObjects;
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private List<Vector3> elementPositions;
    [SerializeField] private List<GameObject> floorElements;
    [SerializeField] private List<Vector3> floorElementPositions;
    
    public TextMeshProUGUI totalText, currentText;
    //[SerializeField] private 
    public int total = 0;
    public int current = 0;
    [SerializeField] bool onFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        total = GetScriptAmount(snapObjects);
        totalText.text = total.ToString();

        for (int i = 0; i < elements.Count; i++)
        {
            elementPositions[i] = elements[i].transform.position;
            //elementPositions[i].transform.rotation = elements[i].transform.rotation;
            floorElementPositions[i] = floorElements[i].transform.position;
            //floorElementPositions[i].transform.rotation = floorElements[i].transform.rotation;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (onFlag == false)
        {
            current = total - GetScriptAmount(snapObjects);
            currentText.text = current.ToString();

            //ToDo:
            //call ResetElements when all the elements have been put together
        }
    }
    //Finish this
    public void ResetElements()
    {
        for (int i = 0; i <  elements.Count; i++)
        {
            elements[i].transform.position = elementPositions[i];
            //elements[i].transform.rotation = elementPositions[i].transform.rotation;
            floorElements[i].transform.position = floorElementPositions[i];
            //floorElements[i].transform.rotation = floorElementPositions[i].transform.rotation;
        }
    }
    private static int GetScriptAmount(SnapPiece[] snapInteractors)
    {
        int count = 0;
        snapInteractors = FindObjectsOfType<SnapPiece>();
        foreach (var item in snapInteractors)
        {
            count += 1;
        }

        return count;
    }
}
