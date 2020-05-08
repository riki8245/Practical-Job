using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{/*
    public GameObject canvas_title;
    public GameObject canvas_settings;
    public GameObject canvas_about;
    public GameObject canvas_buttons;
    public MS_enemyMove mS_EnemyMove;

    private void Awake()
    {
        canvas_title.transform.localScale = new Vector3(1f, 1f, 1f);
        canvas_title.transform.position = new Vector3(canvas_title.transform.position.x, canvas_title.transform.position.y + 20, canvas_title.transform.position.z);

        canvas_settings.transform.localScale = new Vector3(0f, 0f, 1f);
        canvas_about.transform.localScale = new Vector3(0f, 0f, 1f);

        canvas_buttons.transform.position = new Vector3(canvas_buttons.transform.position.x, canvas_buttons.transform.position.y, -10);
        canvas_buttons.transform.localScale = new Vector3(0.1f, 0.0f, 0.1f);
    }
    private void Start()
    {
        iTween.MoveTo(canvas_title, iTween.Hash("y", canvas_title.transform.position.y - 20, "easeType", "easeOutBounce", "time", 2f,"onComplete", "activateButtons"));
    }
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void ShowSettings()
    {
        canvas_title.SetActive(false);
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
        canvas_title.SetActive(true);
    }
    public void activateButtons()
    {
        Debug.Log("EY");
        iTween.ScaleTo(canvas_title, iTween.Hash("x", canvas_title.transform.localScale.x + .02f, "y", canvas_title.transform.localScale.y + .02f, "z", canvas_title.transform.localScale.z + .02f, "easeType", "linear", "time", 3f, "looptype", "pingPong"));
        iTween.MoveTo(canvas_buttons.gameObject, iTween.Hash("z", 4.840001, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(canvas_buttons.gameObject, iTween.Hash("x", 0.1, "y", 0.1, "z", 0.1, "easeType", "easeOutSine", "time", 2f, "oncomplete", "moveButtons"));
    }
    void moveButtons()
    {
        iTween.MoveTo(canvas_buttons.gameObject, iTween.Hash("y", canvas_buttons.transform.position.y + .8f, "time", 3f, "easeType", "linear", "loopType", "pingPong"));
        mS_EnemyMove.enabled = true;
    }*/
}

