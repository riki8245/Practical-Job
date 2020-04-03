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
    public GameObject myPrefab;
    private GameObject boxcopy;
    public float distance;
    public float dis_to_reset_col;



    public bool isPortal_1;

    //This is A mierda, PLEASE check it
    void Start()
    {
        dis_to_reset_col = 0.2f;
        distance = 0.15f;
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
            if (transform.position.y + distance > box.transform.position.y)
            {
                box.transform.position = new Vector3(destination.position.x-distance, destination.position.y, destination.position.z);
                box.GetComponent<Rigidbody>().velocity = new Vector3(-newVelocity.y, newVelocity.x, newVelocity.z);
                //box.GetComponent<BoxCollider>().enabled = true;
                teleported = true;
            }
            if (teleported)
            {
                if(box.transform.position.x > destination.position.x + dis_to_reset_col)
                {
                    boxcopy.active = false;
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
            boxcopy= Instantiate(myPrefab, box.transform.position, box.transform.rotation);
            boxcopy.GetComponent<BoxCollider>().enabled = false;
            boxcopy.GetComponent<Rigidbody>().velocity = box.GetComponent<Rigidbody>().velocity;
        }
    
    }
}                                                                                              