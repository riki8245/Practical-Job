using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    private Transform pass;
    private GameObject box;
    private bool passingTrough;
    private Vector3 newVelocity;
    private bool teleported;



    public bool isPortal_1;
    void Start()
    {
        passingTrough = false;
        teleported = false;
        if (!isPortal_1)
        {
            destination = GameObject.FindGameObjectWithTag("Portal_1").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("Portal_2").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (passingTrough)
        {
            newVelocity = box.GetComponent<Rigidbody>().velocity;
            if (transform.position.y > box.transform.position.y)
            {
                box.transform.position = new Vector3(destination.position.x, destination.position.y, destination.position.z);
                box.GetComponent<Rigidbody>().velocity = new Vector3(-newVelocity.y, newVelocity.x, newVelocity.z);
                //box.GetComponent<BoxCollider>().enabled = true;
                teleported = true;
            }
            if (teleported)
            {
                if(box.transform.position.x > destination.position.x + 0.5f)
                {
                    box.GetComponent<BoxCollider>().enabled = true;
                    passingTrough = false;
                    teleported = false;
                }
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            box = other.gameObject;
            box.GetComponent<BoxCollider>().enabled = false;
            passingTrough = true;
        }
    
    }
}                                                                                              