using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMoving : MonoBehaviour
{
    public GameObject platform;

    private bool canActivateLever = false;

    // Start is called before the first frame update
    void Start()
    {
        platform.GetComponent<MovingPlatform>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivateLever && Input.GetButtonUp("Fire3"))
        {
            if (platform.GetComponent<MovingPlatform>().enabled == false) platform.GetComponent<MovingPlatform>().enabled = true;
            else RestartPlatformPosition();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canActivateLever = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivateLever = false;
        }
    }

    void RestartPlatformPosition()
    {
        platform.GetComponent<MovingPlatform>().target.parent = null;
        platform.transform.position = platform.GetComponent<MovingPlatform>().start;
        platform.GetComponent<MovingPlatform>().target.transform.position = platform.GetComponent<MovingPlatform>().end;
        platform.GetComponent<MovingPlatform>().enabled = false;
    }
}
