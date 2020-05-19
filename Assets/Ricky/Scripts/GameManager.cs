using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("PlayerSettings")]
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int  currentLevel;
    public float[] FOVs;

    private ShadowQuality _originalShadowSettings;

    private void Awake()
    {
        c_enableHDR = true;
        c_enableMSAA = true;
        c_enableShadows = true;
        currentLevel = 1;
        MakeSingleton();
        _originalShadowSettings = QualitySettings.shadows;
    }

    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadGame();
        }
    }

    //void Start()
    //{
    //    Resolution[] resolutions = Screen.resolutions;
    //    foreach (Resolution res in resolutions)
    //    {
    //        print(res.width + "x" + res.height);
    //    }
    //    Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
    //}
    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();
        if(data != null)
        {
            this.c_enableHDR     = data.c_enableHDR;
            this.c_enableMSAA    = data.c_enableMSAA;
            this.c_enableShadows = data.c_enableShadows;
            this.currentLevel    = data.currentLevel;
        }
    }

    public void SaveAndLoad()
    {
        SaveGame();
    }

    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") && !SceneManager.GetSceneByName("PauseMenu").isLoaded) SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu")) {
            if (Camera.main.allowHDR != c_enableHDR) Camera.main.allowHDR = c_enableHDR;
            if (Camera.main.allowMSAA != c_enableMSAA) Camera.main.allowMSAA = c_enableMSAA;
            if (c_enableShadows)
                QualitySettings.shadows = _originalShadowSettings;
            else
                QualitySettings.shadows = ShadowQuality.Disable;
        }
    }
}
