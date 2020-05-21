using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    GameObject lastSelected;

    #region
    public GameObject gTitle;
    public GameObject buttonRightCanvas;
    public GameObject buttonLeftCanvas;
    public GameObject[] eSystem_firstSelected;
    public GameObject eventSystem;
    public GameObject mainCamera;
    public MS_enemyMove mS_EnemyMove;

    public GameObject character;
    public Transform[] characterPath;
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
    private int currentLevel;
    [HideInInspector]public Toggle[] toggles = new Toggle[3];
    #endregion

    private bool lerpCamera;
    public bool changeColor { get; private set; } //Para los sliders

    private void Awake()
    {
        lastSelected = new GameObject();
        currentLevel = 1;
        gTitle.transform.position = new Vector3(gTitle.transform.position.x, gTitle.transform.position.y + 20, gTitle.transform.position.z);
        buttonLeftCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonRightCanvas.transform.localScale = new Vector3(0f, 1f, 1f);
        buttonLeftCanvas.transform.position = new Vector3(buttonLeftCanvas.transform.position.x, buttonLeftCanvas.transform.position.y, buttonLeftCanvas.transform.position.z - 15);
        buttonRightCanvas.transform.position = new Vector3(buttonRightCanvas.transform.position.x, buttonRightCanvas.transform.position.y, buttonRightCanvas.transform.position.z - 15);
        changeColor = false;
        eSystemAbleToSelect = false;
        lerpCamera = false;
        buttonColorAnim = 0f;
        buttonColor = eSystem_firstSelected[0].GetComponent<Button>().colors;
        foreach (GameObject gameObject in toEnableObjects) gameObject.SetActive(false);
        toEnableObjects[0].transform.localScale = new Vector3(0f, 0f, 1f);
        toEnableObjects[1].transform.localScale = new Vector3(0f, 0f, 1f);
        
    }
    private void Start()
    {
        iTween.MoveTo(gTitle, iTween.Hash("y", gTitle.transform.position.y - 20, "easeType", "easeOutBounce", "time", 2f, "onComplete", "StartMovingButtons", "onCompleteTarget",this.gameObject));
        foreach (Toggle t in toggles)
        {
            switch (t.name)
            {
                case "HDRToggle":
                    toggles[0].isOn = GameManager.instance.c_enableHDR;
                    break;
                case "MSAAToggle":
                    toggles[1].isOn = GameManager.instance.c_enableMSAA;
                    break;
                case "Shadows":
                    toggles[2].isOn = GameManager.instance.c_enableShadows;
                    break;
            }
        }
    }

    private void StartMovingButtons()
    {
        iTween.MoveTo(buttonLeftCanvas, iTween.Hash("z", buttonLeftCanvas.transform.position.z + 15, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(buttonLeftCanvas, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", "easeOutSine", "time", 2f, "oncomplete", "MoveButtons", "onCompleteTarget", this.gameObject));
        iTween.MoveTo(buttonRightCanvas, iTween.Hash("z", buttonRightCanvas.transform.position.z + 15, "easeType", "easeOutBounce", "time", 2f));
        iTween.ScaleTo(buttonRightCanvas, iTween.Hash("x", 1, "y", 1, "z", 1, "easeType", "easeOutSine", "time", 2f));
    }

    private void MoveButtons()
    {
        Able_eSystemInput("MainMenu");
        iTween.MoveTo(buttonLeftCanvas, iTween.Hash("name", "buttonsLeft","y", buttonLeftCanvas.transform.position.y + .8f, "time", 3f, "easeType", "linear", "loopType", "pingPong"));
        iTween.MoveTo(buttonRightCanvas, iTween.Hash("name", "buttonsRight","y", buttonRightCanvas.transform.position.y + .8f, "time", 3f, "easeType", "linear", "loopType", "pingPong"));
        
        changeColor = true;
        mS_EnemyMove.enabled = true;
    }
    
    public void StartGame()
    {
        if (eSystemAbleToSelect)
        {
            eSystemAbleToSelect = false;
            foreach (Transform transform in characterPath) transform.position = new Vector3(transform.position.x, character.transform.position.y, transform.position.z);
            iTween.RotateTo(character, iTween.Hash("y", -180f, "easeType", "easeInOutQuad", "time", 1f, "oncomplete", "MoveCharacter", "onCompleteTarget", this.gameObject));
        }
    }
    
    private void MoveCharacter()
    {
        character.GetComponent<Animator>().SetFloat("speed", 1f);
        iTween.MoveTo(character, iTween.Hash("Path", characterPath, "easeType", "linear", "time", 1f));
        iTween.RotateTo(character, iTween.Hash("y", -90f, "easeType", "easeInOutQuad", "oncomplete", "LoadLevelSelector", "time", 1f, "onCompleteTarget", this.gameObject));
    }

    private void LoadLevelSelector()
    {
        character.GetComponent<Animator>().SetFloat("speed", 0f);
        this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        foreach (GameObject gameObj in toDisableObjects) iTween.ScaleTo(gameObj, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutSine", "time", 1f, "onComplete", "DisableObjects", "onCompleteTarget", this.gameObject, "onCompleteParams", gameObj));
        iTween.MoveTo(mainCamera, iTween.Hash("x",-3f,"y", 7.75f,"z", 5.75f,"time", 1f,"easeType","linear"));
        iTween.RotateTo(mainCamera, iTween.Hash("x", 31.8, "y", -90f,"time", 1f, "easeType", "linear"));
        lerpCamera = true;
    }

    public void NewMenuOpen()
    {
        if (eSystemAbleToSelect)
        {
            eSystemAbleToSelect = false;
            if (iTween.tweens.Contains(iTween.Hash("name", "buttonsLeft"))) iTween.StopByName("buttonsLeft");
            if (iTween.tweens.Contains(iTween.Hash("name", "buttonsRight"))) iTween.StopByName("buttonsRight");
            foreach (GameObject gameObj in toDisableObjects) iTween.ScaleTo(gameObj, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutSine", "time", .5f, "onComplete", "DisableObjects", "onCompleteTarget", this.gameObject, "onCompleteParams", gameObj));
        }
    }

    private void Update()
    {
        try
        {
            if (eSystemAbleToSelect)
            {
                eSystem_currentSelected = this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
                if (lastSelected != eSystem_currentSelected)
                {
                    lastSelected = eSystem_currentSelected;
                    AudioController.AudioInstance.soundMenuPop(true);
                }
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
                buttonColor.selectedColor = Color.Lerp(new Color(0f, 0f, 0f, 0f), new Color(245f, 245f, 245f, 1f), buttonColorAnim);
                eSystem_firstSelected[0].GetComponent<Button>().colors = buttonColor;
                if (buttonColor.selectedColor.Equals(Color.white)) changeColor = false;
                else if (buttonColorAnim < 1f) buttonColorAnim += Time.deltaTime / 2f;
            }
            if (lerpCamera)
            {
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 27.3f, 2f * Time.deltaTime);
                if (Camera.main.fieldOfView - 27.3f < .5f)
                {
                    lerpCamera = false;
                    Able_eSystemInput("Levels");
                }
            }
        }
        catch (System.NullReferenceException) { }
    }

    private void LoadSettingsMenu()
    {
        toEnableObjects[0].SetActive(true);
        iTween.ScaleTo(toEnableObjects[0].transform.gameObject, iTween.Hash("x", 1, "y", 1.4, "z", 1, "easeType", "easeOutSine", "time", .5f,"onComplete", "Able_eSystemInput", "onCompleteTarget", this.gameObject, "onCompleteParams", "Settings"));
    }

    private void LoadAboutMenu()
    {
        toEnableObjects[1].SetActive(true);
        iTween.ScaleTo(toEnableObjects[1].transform.gameObject, iTween.Hash("x", 1, "y", 1.4, "z", 1, "easeType", "easeOutSine", "time", .5f, "onComplete", "Able_eSystemInput", "onCompleteTarget", this.gameObject, "onCompleteParams", "About"));
    }
    private void Able_eSystemInput(string toSelectMenu)
    {
        switch (toSelectMenu)
        {
            case "Settings":
                this.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[1];
                this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[1]);
                break;
            case "About":
                this.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[2];
                this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[2]);
                break;
            case "MainMenu":
                eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[0];
                eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[0]);
                break;
            case "Levels":
                this.eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = eSystem_firstSelected[3];
                this.eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(eSystem_firstSelected[3]);
                RotateCassete.eventSystemBool = true;
                break;
        }
        eSystemAbleToSelect = true;
        lastSelected = this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        if(eSystemAbleToSelect)
            StartCoroutine(LoadYourAsyncScene(0));
        eSystemAbleToSelect = false;
    }
    public void ReturnMainMenuFromMenus()
    {
        
        foreach (GameObject gameObject in toEnableObjects)
            if (gameObject.activeInHierarchy) iTween.ScaleTo(gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 1f, "time", 1f, "onComplete", "ReturnMainMenuAnim", "onCompleteTarget", this.gameObject, "onCompleteParams", gameObject));
    }
    private void ReturnMainMenuAnim(GameObject gameObject)
    {
        gameObject.SetActive(false);
        foreach (GameObject gameObject1 in toDisableObjects)
        {
            gameObject1.SetActive(true);
            if (gameObject1.name.Equals("Title")) iTween.ScaleTo(gameObject1, iTween.Hash("x", 5f, "y", 5f, "z", 5f, "time", 1f));
            else iTween.ScaleTo(gameObject1, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "time", 1f));

        }
        StartCoroutine(ReloadMainMenu());
    }
    IEnumerator ReloadMainMenu()
    {
        yield return new WaitForSeconds(1);
        MoveButtons();
    }

    IEnumerator LoadYourAsyncScene(int scene)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone){yield return null;}
    }

    private void DisableObjects(GameObject game)
    {
        game.SetActive(false);
        try
        {
            if (game.name.Equals(toDisableObjects[toDisableObjects.Length - 1].name))
            {
                if (this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name.Equals("Settings")) LoadSettingsMenu();
                else if (this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.name.Equals("About")) LoadAboutMenu();
            }
        }
        catch (System.NullReferenceException) { }  
    }
    public void SelectAntiAlisingMode(TextMeshProUGUI textMesh)
    {
        switch (textMesh.text)
        {
            case "None":
                textMesh.SetText("FXAA");
                GameManager.instance.c_Antialising = 4;
                break;
            case "FXAA":
                textMesh.SetText("SMAA");
                GameManager.instance.c_Antialising = 8;
                break;
            case "SMAA":
                textMesh.SetText("None");
                GameManager.instance.c_Antialising = 0;
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
        GameManager.instance.c_enableHDR = c_enableHDR;
    }
    public void EnableMSAA(Toggle toggle)
    {
        c_enableMSAA = toggle.isOn;
        GameManager.instance.c_enableMSAA = c_enableMSAA;
    }
    public void EnableShadows(Toggle toggle)
    {
        c_enableShadows = toggle.isOn;
        GameManager.instance.c_enableShadows = c_enableShadows;
    }
    public void SelectLevel(GameObject gameObject)
    {
        currentLevel = int.Parse(gameObject.name.Substring(5));
        if (eSystemAbleToSelect)
            StartCoroutine(LoadYourAsyncScene(currentLevel));
        eSystemAbleToSelect = false;
    }    
}
