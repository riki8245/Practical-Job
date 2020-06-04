using System.Collections;
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
            door.SetBool("open", false);
            if (timeToPassToNextLevel > 1.5f)
            {

                if (SceneManager.GetActiveScene().buildIndex == 11) //If game complete, return to menu
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    GameManager.instance.currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
                    GameManager.instance.SaveGame();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerReachFinish = true;
            AudioController.AudioInstance.StopAllSounds();
            other.gameObject.GetComponent<PlayerControl>().enabled = false;
        }
    }
}
