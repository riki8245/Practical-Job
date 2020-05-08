using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject[] toDisableObjects;
    public GameObject[] toEnableObjects;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowSettings()
    {
        iTween.StopByName("title");
        iTween.StopByName("buttons");
        foreach(GameObject gameObj in toDisableObjects)
        {
            gameObj.SetActive(false);
        }
        foreach (GameObject gameObj in toEnableObjects)
        {
            if(gameObj.name.Equals("SettingsMenu"))gameObj.SetActive(true);
        }
    }
    public void ShowAbout()
    {
        foreach (GameObject gameObj in toDisableObjects)
        {
            gameObj.SetActive(false);
        }
        foreach (GameObject gameObj in toEnableObjects)
        {
            if (gameObj.name.Equals("AboutMenu")) gameObj.SetActive(true);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ReturnMainMenu()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
