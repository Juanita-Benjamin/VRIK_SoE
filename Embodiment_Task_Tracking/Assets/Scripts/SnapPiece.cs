using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SnapPiece : MonoBehaviour
{
    public string tag;
    private Collider collider;
    private AudioSource source;
    public int counter = 0;
    private bool isSnapped = false;

    // Start is called before the first frame update
    private Dictionary<GameObject, string> originalTags = new Dictionary<GameObject, string>();
    private void Start()
    {
        collider = GetComponent<Collider>();
      

    }

    // Update is called once per frame
    private void Update()
    {
    }
  

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(tag))
        {
           
            //Setting the parent will change the scale: DO NOT DO!
            //other.gameObject.transform.SetParent(gameObject.transform);
            
            //then stop the physics?
            Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
            Rigidbody parent = gameObject.GetComponent<Rigidbody>();

            body.constraints = RigidbodyConstraints.FreezeAll;
            body.useGravity = false;
            body.isKinematic = true;

            //DONT RESET The Tag or else it won't work
            other.gameObject.tag = "Untagged";
            counter++;

            ElementReset resetScript = FindObjectOfType<ElementReset>();
            if (resetScript != null)
            {
                resetScript.IncrementCounter();
                
            }
        }
    }

    //Not used
    private void InitAudio(Collider other)
    {
        source = other.gameObject.AddComponent<AudioSource>();
        source.clip = Resources.Load("Sounds/Pop") as AudioClip;
        source.pitch = 0.9f;
        source.spatialBlend = 1f;
    }

    public void StoreTag(GameObject obj)
    {
        if (!originalTags.ContainsKey(obj))
        {
            originalTags[obj] = obj.tag;
        }
    }

    public void ResetTag(GameObject obj)
    {
        if (originalTags.TryGetValue(obj, out string originalTag))
        {
            obj.tag = originalTag; 
        }
    }

    //This is not needed or used
    public void ResetAllTags()
    {
        foreach (var kvp in originalTags)
        {
            if (kvp.Key != null)
            {
                kvp.Key.tag = kvp.Value;
            }
        }
        originalTags.Clear();
    }
}