using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Hay que poner las escenas en orden al hacer en el build configurations para que lo coja bien
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
