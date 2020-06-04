using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLevel : MonoBehaviour
{
    public static bool playerReachFinish = false;
    private float timeToPassToNextLevel;
    private Animator door;
    // Start is called before the first frame update
    private void Awake()
    {
        door = gameObject.GetComponentInChildren<Animator>();
        playerReachFinish = false;
    }
    void Start()
    {
        playerReachFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerReachFinish)
        {
            timeToPassToNextLevel += Time.deltaTime;
            if (timeToPassToNextLevel > 1.5f)
            {

                if (SceneManager.GetActiveScene().buildIndex == 11) //If game complete, return to menu
                {
                    SceneManager.LoadSceneAsync(0);
                }
                else
                {
                    GameManager.instance.currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
                    GameManager.instance.SaveGame();
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
    private void LoadFade()
    {
        SceneManager.LoadScene("FadeScene", LoadSceneMode.Additive);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerReachFinish = true;
            Invoke("LoadFade", 1f);
            AudioController.AudioInstance.StopAllSounds();
            other.gameObject.GetComponent<PlayerControl>().enabled = false;
            door.SetBool("open", false);
        }
    }
}
