using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public GameObject titleCanvas;
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void ShowSettings()
    {
        titleCanvas.SetActive(false);
    }
    void ShowAbout()
    {

    }
    void ExitGame()
    {
        Application.Quit();
    }
    void ReturnMainMenu()
    {
        titleCanvas.SetActive(true);
    }
}

