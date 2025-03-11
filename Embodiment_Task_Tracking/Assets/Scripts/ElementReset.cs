using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Movement.BodyTrackingForFitness;

public class ElementReset : MonoBehaviour
{
    [SerializeField] private SnapPiece[] snapObjects;
    [SerializeField] private List<GameObject> elements;
    [SerializeField] private List<Vector3> elementPositions;
    [SerializeField] private List<GameObject> floorElements;
    [SerializeField] private List<Vector3> floorElementPositions;
    public GameObject breakScreen;

    public TextMeshProUGUI totalText, currentText;

    //[SerializeField] private
    public int total = 0;

    public int current = 0;

    private SnapPiece snapNumber;
    [SerializeField] private bool onFlag = false;

    // Start is called before the first frame update
    private void Start()
    {
      
        total = GetScriptAmount(snapObjects);
        totalText.text = total.ToString();

        for (int i = 0; i < elements.Count; i++)
        {
            elementPositions[i] = elements[i].transform.position;
            floorElementPositions[i] = floorElements[i].transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
        //current = total - GetScriptAmount(snapObjects);
        currentText.text = current.ToString();

        if (current == total)
        {
            //set the break screen active and call reset
            breakScreen.SetActive(true);
            current = 0; //set current to 0 again
           
        }
    }

    //Finish this
    public void ResetElements()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].transform.position = elementPositions[i];

            floorElements[i].transform.position = floorElementPositions[i];
        }

        current = 0;
        breakScreen.SetActive(false);
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

    public void IncrementCounter()
    {
        current++;
    }
}