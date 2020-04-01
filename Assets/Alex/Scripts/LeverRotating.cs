using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotating : MonoBehaviour
{
    public GameObject platform;

    private float smoothTime = 30f;
    private float velocity;
    private bool canActivateLever = false;
    private bool isRotating = false;
    private float newRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivateLever && Input.GetButtonUp("Fire3") && !isRotating)
        {
            newRotation = platform.transform.localRotation.y + 90f;
            platform.GetComponent<MovingPlatform>().target.parent = null;
            StartCoroutine(RotateMe(Vector3.up * 90, 0.2f));
            isRotating = true; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = platform.transform.rotation;
        var toAngle = Quaternion.Euler(platform.transform.eulerAngles + byAngles);
        for (var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            platform.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        isRotating = false;
        platform.transform.rotation = toAngle;
    }
}
