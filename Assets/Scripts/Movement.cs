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

    [SerializeField]
    AudioClip mainEngineAudio;

    [SerializeField]
    ParticleSystem mainThrusterParticles;

    [SerializeField]
    ParticleSystem leftThrusterParticles;

    [SerializeField]
    ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineAudio);
            }
            if (!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrusterParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(Vector3.back);
            // transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            ApplyRotation(rotationThrust);
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // transform.Rotate(Vector3.back);
            // transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
            ApplyRotation(-rotationThrust);
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        } else 
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation, so we can manually rotate!
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation, so the physics system can take over!
    }
}
