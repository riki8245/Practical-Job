using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Con esta variable podemos controlar también si queremos pausar la música que pongamos
    public GameObject player;
    public GameObject pauseMenuIU;
    public Button resumeButton;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button7)) // Hay que cambiarlo por el botón de Start
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //if(resumeButton.)
        pauseMenuIU.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuIU.SetActive(true);
        Time.timeScale = 0f;
        //resumeButton.Select();
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
