using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    public GameObject gTitle;
    public GameObject buttonRightCanvas;
    public GameObject buttonLeftCanvas;
    public GameObject[] eSystem_firstSelected;
    public GameObject eventSystem;
    public MS_enemyMove mS_EnemyMove;

    public GameObject[] toDisableObjects;
    public GameObject[] toEnableObjects;
    private ColorBlock buttonColor;
    private float buttonColorAnim;
    private GameObject eSystem_currentSelected;
    public Image[] backgroundImgs;
    private bool eSystemAbleToSelect;
    public string[] Resolutions;
    private bool c_enableHDR;
    private bool c_enableMSAA;
    private bool c_enableShadows;

    public bool changeColor { get; private set; }

    private void Awake()
    {
        gTitle.transform.position = new Vector3(gTitle.transform.position.x, gTitle.transform.position.y + 20, gTitle.transform.position.z);
        buttonRightCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonRightCanvas.transform.position = new Vector3(buttonRightCanvas.transform.position.x, buttonRightCanvas.transform.position.y, buttonRightCanvas.transform.position.z - 15);
        buttonLeftCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonLeftCanvas.transform.position = new Vector3(buttonLeftCanvas.transform.position.x, buttonLeftCanvas.transform.position.y, buttonLeftCanvas.transform.position.z - 15);
        changeColor = false;
        eSystemAbleToSelect = false;
        buttonColorAnim = 0f;
        buttonColor = eSystem_firstSelected[0].GetComponent<Button>().colors;
        foreach (GameObject gameObject in toEnableObjects) gameObject.SetActive(false);
        toEnableObjects[0].transform.localScale = new Vector3(0f, 0f, 1f);
        toEnableObjects[1].transform.localScale = new Vector3(0f, 0f, 1f);

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
        eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[0];
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[0]);
        eSystemAbleToSelect = true;
        changeColor = true;
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
        foreach(GameObject gameObj in toDisableObjects) iTween.ScaleTo(gameObj, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutSine", "time", 1f, "onComplete", "DisableObjects", "onCompleteTarget",this.gameObject,"onCompleteParams",gameObj));
    }
    private void Update()
    {
        if (eSystemAbleToSelect)
        {
            eSystem_currentSelected = this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
            if (eSystem_currentSelected.name.Equals("VolumeSlider"))
            {
                backgroundImgs[0].color = new Color(0, 248, 250, 255);
                backgroundImgs[1].color = Color.white;

            }
            else if (eSystem_currentSelected.name.Equals("SFXSlider"))
            {
                backgroundImgs[1].color = new Color(0, 248, 250, 255);
                backgroundImgs[0].color = Color.white;
            }
            else
            {
                backgroundImgs[1].color = Color.white;
                backgroundImgs[0].color = Color.white;
            }
        }
        if (changeColor)
        {
            buttonColor.selectedColor = Color.Lerp(new Color(0f, 0f, 0f, 0f), new Color(245f,245f,245f,1f), buttonColorAnim);
            eSystem_firstSelected[0].GetComponent<Button>().colors = buttonColor;
            if (buttonColor.selectedColor.Equals(Color.white)) changeColor = false;
            else if(buttonColorAnim < 1f) buttonColorAnim += Time.deltaTime/2f;
        }
        
    }
    private void LoadSettingsMenu()
    {
        toEnableObjects[0].SetActive(true);
        iTween.ScaleTo(toEnableObjects[0].transform.gameObject, iTween.Hash("x", 1, "y", 1.4, "z", 1, "easeType", "easeOutSine", "time", .5f));
        this.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[1];
        this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[1]);
    }
    private void LoadAboutMenu()
    {
        toEnableObjects[1].SetActive(true);
        iTween.ScaleTo(toEnableObjects[1].transform.gameObject, iTween.Hash("x", 1, "y", 1.4, "z", 1, "easeType", "easeOutSine", "time", .5f));
        this.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[2];
        this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[2]);
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
    private void DisableObjects(GameObject game)
    {
        game.SetActive(false);
        if (game.name.Equals(toDisableObjects[toDisableObjects.Length - 1].name))
        {
            if (this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name.Equals("Settings")) LoadSettingsMenu();
            else LoadAboutMenu();
        }
    }
    public void SelectAntiAlisingMode(TextMeshProUGUI textMesh)
    {
        switch (textMesh.text)
        {
            case "None":
                textMesh.SetText("FXAA");
                break;
            case "FXAA":
                textMesh.SetText("SMAA");
                break;
            case "SMAA":
                textMesh.SetText("None");
                break;
            default:
                break;
        }
    }
    public void SelectResolutionMode()
    {

    }
    public void EnableHDR(Toggle toggle)
    {
        c_enableHDR = toggle.isOn;
    }
    public void EnableMSAA(Toggle toggle)
    {
        c_enableMSAA = toggle.isOn;
    }
    public void EnableShadows(Toggle toggle)
    {
        c_enableShadows = toggle.isOn;
    }
}
