using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneToLoad; // Name of the scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Change "Player" to the tag of the object you want to trigger the scene change
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
