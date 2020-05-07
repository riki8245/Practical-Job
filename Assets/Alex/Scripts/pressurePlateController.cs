using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressurePlateController : MonoBehaviour
{
    public bool pressed = false;
    private Vector3 scaleNotPressed = new Vector3(0.67f, 0.07f, 0.67f);
    private Vector3 scalePressed = new Vector3(0.67f, 0.01f, 0.67f);
    public GameObject canvas;
    private string actualScene;

    public Animator door;

    // Start is called before the first frame update
    void Start()
    {
        actualScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            transform.localScale = scalePressed;
        }
        else
        {
            transform.localScale = scaleNotPressed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            pressed = true;
            door.SetBool("open",true);
            if(other.gameObject.CompareTag("Box"))
                other.GetComponent<BoxController>().b_moveDirection = Vector3.zero;
            if (other.gameObject.CompareTag("Player"))
            {
                canvas.SetActive(true);
                canvas.GetComponent<Animator>().SetTrigger("Play");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!actualScene.Equals("Level 1"))
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
            {
                pressed = false;
                door.SetBool("open", false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            pressed = true;
            door.SetBool("open", true);
        }
    }

}
