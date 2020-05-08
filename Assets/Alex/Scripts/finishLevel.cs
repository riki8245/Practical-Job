using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLevel : MonoBehaviour
{
    public static bool playerReachFinish = false;
    private float timeToPassToNextLevel;
    public Animator door;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerReachFinish)
        {
            timeToPassToNextLevel += Time.deltaTime;
            door.SetBool("open", false);
            if (timeToPassToNextLevel > 2) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerReachFinish = true;
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
