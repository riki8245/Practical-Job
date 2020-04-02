using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    private Transform pass;

    public bool isPortal_1;
    void Start()
    {
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
        
    }
}                                                                                              