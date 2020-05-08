using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject gTitle;
    public GameObject buttonRightCanvas;
    public GameObject buttonLeftCanvas;
    public MS_enemyMove mS_EnemyMove;

    public GameObject[] toDisableObjects;
    public GameObject[] toEnableObjects;


    private void Awake()
    {
        gTitle.transform.position = new Vector3(gTitle.transform.position.x, gTitle.transform.position.y + 20, gTitle.transform.position.z);
        buttonRightCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonRightCanvas.transform.position = new Vector3(buttonRightCanvas.transform.position.x, buttonRightCanvas.transform.position.y, buttonRightCanvas.transform.position.z - 15);
        buttonLeftCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonLeftCanvas.transform.position = new Vector3(buttonLeftCanvas.transform.position.x, buttonLeftCanvas.transform.position.y, buttonLeftCanvas.transform.position.z - 15);

    }
    private void Start()
    {
        iTween.MoveTo(gTitle, iTween.Hash("y", gTitle.transform.position.y - 20, "easeType", "easeOutBounce", "time", 2f, "onComplete", "StartMovingButtons", "onCompleteTarget",this.gameObject));
    }
    private void StartMovingButtons()
    {
        iTween.MoveTo(buttonLeftCanvas.transform.gameObject, iTween.Hash("z", buttonLeftCanvas.transform.position.z + 15, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(buttonLeftCanvas.transform.gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", "easeOutSine", "time", 2f, "oncomplete", "MoveButtons", "onCompleteTarget", this.gameObject));
        iTween.MoveTo(buttonRightCanvas.transform.gameObject, iTween.Hash("z", buttonRightCanvas.transform.position.z + 15, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(buttonRightCanvas.transform.gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", "easeOutSine", "time", 2f));
  
    }
    private void MoveButtons()
    {
        iTween.MoveTo(buttonLeftCanvas.transform.gameObject, iTween.Hash("name", "buttonsLeft","y", buttonLeftCanvas.gameObject.transform.position.y + .8f, "time", 3f, "easeType", "linear", "loopType", "pingPong"));
        iTween.MoveTo(buttonRightCanvas.transform.gameObject, iTween.Hash("name", "buttonsRight","y", buttonRightCanvas.gameObject.transform.position.y + .8f, "time", 3f, "easeType", "linear", "loopType", "pingPong"));

        mS_EnemyMove.enabled = true;
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void NewMenuOpen()
    {
        iTween.StopByName("buttonsLeft");
        iTween.StopByName("buttonsRight");
        foreach(GameObject gameObj in toDisableObjects) iTween.ScaleTo(gameObj, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutSine", "time", 1f, "onComplete", "SetActiveObjects","onCompleteTarget",this.gameObject,"onCompleteParams",gameObj));
        if (EventSystem.current.currentSelectedGameObject.name.Equals("Settings")) toEnableObjects[1].SetActive(true);
        else toEnableObjects[2].SetActive(true);
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
    private void SetActiveObjects(GameObject game)
    {
        game.SetActive(false);
        toEnableObjects[0].SetActive(true);

    }
}
