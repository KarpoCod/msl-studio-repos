using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Constraint : MonoBehaviour
{
    public Transform currentPlanet;

    
    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.FromToRotation(-transform.up, currentPlanet.position - transform.position);
        transform.rotation = rotation * transform.rotation;
    }
}
