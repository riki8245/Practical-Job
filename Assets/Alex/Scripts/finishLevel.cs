using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLevel : MonoBehaviour
{
    public static bool playerReachFinish = false;
    private Animator door;
    // Start is called before the first frame update
    private void Awake()
    {
        door = gameObject.GetComponentInChildren<Animator>();
    }
  
    private void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11) SceneManager.LoadSceneAsync(0);
        else
        {
            if(GameManager.instance) GameManager.instance.currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if (GameManager.instance) GameManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);
        }
    }
    private void LoadFade()
    {
        SceneManager.LoadScene("FadeScene",LoadSceneMode.Additive);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (AudioController.AudioInstance) AudioController.AudioInstance.StopAllSounds();
            other.gameObject.GetComponent<PlayerControl>().enabled = false;
            door.SetBool("open", false);
            LoadFade();
            Invoke("LoadNextLevel", 1f);
        }
    }
}
