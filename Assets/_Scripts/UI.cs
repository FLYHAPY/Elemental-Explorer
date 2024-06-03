using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject image;
    public GameObject text;
    public GameObject text2;
    public GameObject button;
    public GameObject button2;
    public GameObject player;
    public void PlayButton()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void No()
    {
        image.SetActive(false);
        text.SetActive(false);
        text2.SetActive(false);
        button.SetActive(false);
        button2.SetActive(false);
        player.GetComponent<ElementsCharacter>().ispaused = false;
    }
}
