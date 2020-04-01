using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMoving : MonoBehaviour
{
    public GameObject platform;

    private bool canActivateLever = false;
    private bool movePlatformToOrigin = false;

    // Start is called before the first frame update
    void Start()
    {
        platform.GetComponent<MovingPlatform>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivateLever && Input.GetButtonUp("Fire3") && !movePlatformToOrigin)
        {
            if (platform.GetComponent<MovingPlatform>().enabled == false) platform.GetComponent<MovingPlatform>().enabled = true;
            else RestartPlatformPosition();
        }

        if(movePlatformToOrigin)
        {
            platform.transform.position = Vector3.Slerp(platform.transform.position, platform.GetComponent<MovingPlatform>().start, 0.8f);
            if (platform.transform.position == platform.GetComponent<MovingPlatform>().start) movePlatformToOrigin = false;
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
        movePlatformToOrigin = true;
        platform.GetComponent<MovingPlatform>().target.parent = null;
        //platform.transform.position = platform.GetComponent<MovingPlatform>().start;
        platform.GetComponent<MovingPlatform>().target.transform.position = platform.GetComponent<MovingPlatform>().end;
        platform.GetComponent<MovingPlatform>().enabled = false;
    }
}
