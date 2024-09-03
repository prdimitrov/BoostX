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
                break;
            default:
                Debug.Log("Player just crashed into something!");
                ReloadLevel();
                break;
        }
    }

    private void ReloadLevel()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
