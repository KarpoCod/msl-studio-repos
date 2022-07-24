using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller1 : MonoBehaviour
{

    public int speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Vector3 forward = transform.forward * Input.GetAxis("Vertical")*4;
        Vector3 right = transform.right * Input.GetAxis("Horizontal")*4;

        rb.AddForce((forward + right)*Time.fixedDeltaTime*speed);
    }
}
