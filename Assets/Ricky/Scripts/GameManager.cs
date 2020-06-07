using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("PlayerSettings")]
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int c_Antialising;
    public int  currentLevel;
    public float SfxVolume;
    public float MusicVolume;
    public float[] FOVs;
    int sceneN;
    float resetTimer = 0f;

    private ShadowQuality _originalShadowSettings;

    private void Awake()
    {
        c_enableHDR = true;
        c_enableMSAA = true;
        c_enableShadows = true;
        c_Antialising = 4;
        currentLevel = 1;
        SfxVolume = 0.5f;
        MusicVolume = 0.0f;
        MakeSingleton();
        _originalShadowSettings = QualitySettings.shadows;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
            this.c_Antialising   = data.c_Antialising;
            this.SfxVolume       = data.a_SfxVolume;
            this.MusicVolume     = data.a_MusicVolume;
        }
    }

    public void SaveAndLoad()
    {
        SaveGame();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    private void LoadFade()
    {
        SceneManager.LoadScene("FadeScene", LoadSceneMode.Additive);
    }

    void Update()
    {
        //if(currentLevel != SceneManager.GetActiveScene().buildIndex) currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (!SceneManager.GetActiveScene().name.Equals("PLAY") && !SceneManager.GetSceneByName("PauseMenu").isLoaded) SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        if (!SceneManager.GetActiveScene().name.Equals("PLAY")) {
            if (SceneManager.GetActiveScene().buildIndex != sceneN)
            {
                sceneN = SceneManager.GetActiveScene().buildIndex;
                Camera.main.allowHDR  = c_enableHDR;
                Camera.main.allowMSAA = c_enableMSAA;
                if (c_enableShadows) QualitySettings.shadows = _originalShadowSettings;
                else QualitySettings.shadows = ShadowQuality.Disable;
                QualitySettings.antiAliasing = c_Antialising;
            }
            if (Input.GetButton("Fire4"))
            {
                if(resetTimer >= 2f)
                {
                    LoadFade();
                    Invoke("ReloadLevel", 1f);
                    if (AudioController.AudioInstance) AudioController.AudioInstance.StopAllSounds();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = false;
                    resetTimer = 0f;
                }
                else
                {
                    resetTimer += Time.deltaTime;
                }
            }
        }
    }
}
