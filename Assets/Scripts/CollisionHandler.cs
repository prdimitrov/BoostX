using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Player touched something friendly!");
                break;
            case "Finish":
                Debug.Log("Player reached the finish line!");
                LoadNextLevel();
                break;
            default:
                Debug.Log("Player just crashed into something!");
                ReloadLevel();
                break;
        }
    }

    private static void LoadNextLevel()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = currentSceneIndex + 1;
       if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
       {
        nextSceneIndex = 0;
       }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
