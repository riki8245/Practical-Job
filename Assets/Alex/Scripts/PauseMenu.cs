using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false; // Con esta variable podemos controlar también si queremos pausar la música que pongamos
    public GameObject pauseMenuIU;
    public GameObject firstButton;

    void Update()
    {
        if(Input.GetButtonDown("EscapeButton")) // Hay que cambiarlo por el botón de Start
        {
            if(GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuIU.SetActive(false);
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in enemies)
        {
            item.GetComponent<EnemyController>().enabled = true;
        }
        foreach (GameObject item in characters)
        {
            item.GetComponent<PlayerControl>().enabled = true;
            item.GetComponent<AnimationScript>().enabled = true;
        }
        GameIsPaused = false;
        Time.timeScale = 1f;

    }

    void Pause()
    {
        StartCoroutine(FixEventSystem());
        pauseMenuIU.transform.localScale = new Vector3(0f, 0f, 0f);
        pauseMenuIU.SetActive(true);
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in enemies)
        {
            item.GetComponent<EnemyController>().itsounds = false;
            item.GetComponent<EnemyController>().enabled = false;
        }
        foreach (GameObject item in characters)
        {
            item.GetComponent<PlayerControl>().enabled = false;
            item.GetComponent<AnimationScript>().itsounds = false;
            item.GetComponent<AnimationScript>().enabled = false;
        }
        AudioController.AudioInstance.StopAllSounds();
        GameIsPaused = true;
        Time.timeScale = 0f;
        iTween.ScaleTo(pauseMenuIU,iTween.Hash("x",1f,"y",1f,"z",1f,"time",.2f,"onComplete","OnAnimationEnded","onCompleteTarget",this.gameObject,"ignoretimescale",true));
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
   void OnAnimationEnded()
    {
        
    }
    public void ExitGame() { Application.Quit(); }
    IEnumerator FixEventSystem()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(firstButton);

    }
}
