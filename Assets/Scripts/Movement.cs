using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float mainThrust = 1000f;

    [SerializeField]
    float rotationThrust = 160f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // rb.AddRelativeForce(0, 1, 0);
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //It's the same thing!
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(Vector3.back);
            // transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // transform.Rotate(Vector3.back);
            // transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation, so we can manually rotate!
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation, so the physics system can take over!
    }
}
