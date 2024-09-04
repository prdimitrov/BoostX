using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float timeDelay = 1f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Player touched something friendly!");
                break;
            case "Finish":
                Debug.Log("Player reached the finish line!");
                DisableMovement();
                Invoke("LoadNextLevel", timeDelay);
                break;
            default:
                Debug.Log("Player just crashed into something!");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        DisableMovement();
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
