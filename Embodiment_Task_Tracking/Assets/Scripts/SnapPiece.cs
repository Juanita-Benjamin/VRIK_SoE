using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPiece : MonoBehaviour
{
    public string tag;
    private Collider collider;
    private AudioSource source;

    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform == objectAttach)
    //    {
    //        AttachObject();
    //        InitAudio(other);

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(tag))
        {
            

            InitAudio(other);

            source.Play();

            //then stop the physics?
            Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;

            body.constraints = RigidbodyConstraints.FreezeAll;
            body.isKinematic = true;
        }
    }

    private void InitAudio(Collider other)
    {
        source = other.gameObject.AddComponent<AudioSource>();
        source.clip = Resources.Load("Sounds/Pop") as AudioClip;
        source.pitch = 0.9f;
        source.spatialBlend = 1f;
    }
}