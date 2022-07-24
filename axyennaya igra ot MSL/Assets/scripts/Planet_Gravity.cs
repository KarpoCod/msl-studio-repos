using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Gravity : MonoBehaviour
{

    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();

    private Rigidbody rb;

    public float G = 10000000;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Remove(other.attachedRigidbody);
        }
    }

    void FixedUpdate()
    {
        foreach(Rigidbody body in affectedBodies)
        {
            Vector3 directionToPlanet = (transform.position - body.position).normalized;

            float distance = (transform.position - body.position).magnitude;
            float strenght = body.mass * rb.mass / (distance*distance) * G;


            body.AddForce(strenght * directionToPlanet);
        }
    }
}
