using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelay = 1f;
    [SerializeField] AudioClip crashingAudio;
    [SerializeField] AudioClip successLandingAudio;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // This will toggle collisions!
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Player touched something friendly!");
                break;
            case "Finish":
                Debug.Log("Player reached the finish line!");
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Player just crashed into something!");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticles.Play();
        DisableMovement();
        audioSource.PlayOneShot(successLandingAudio);
        Invoke("LoadNextLevel", timeDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        DisableMovement();
        audioSource.PlayOneShot(crashingAudio);
        Invoke("ReloadLevel", timeDelay);
    }

    void LoadScene(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        LoadScene(nextSceneIndex);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }

    void ReloadLevel()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
