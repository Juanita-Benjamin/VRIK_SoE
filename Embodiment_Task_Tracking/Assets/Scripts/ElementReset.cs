using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementReset : MonoBehaviour
{
    [SerializeField]private SnapPiece[] snapObjects;
    public int total = 0;
    public int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        total = GetScriptAmount(snapObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
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
