using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest1 : MonoBehaviour
{
    private BoxCollider[] b_Colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HOLA");
        if (other.CompareTag("Box"))
            DisableBoxColliders(other); 
    }

    private void DisableBoxColliders(Collider other)
    {
        Debug.Log("hola");
        b_Colliders = other.gameObject.GetComponents<BoxCollider>();
        foreach(BoxCollider boxCollider in b_Colliders){
            boxCollider.enabled = false;
        }
        other.gameObject.GetComponent<CharacterController>().isTrigger = true;

        other.gameObject.GetComponent<CharacterController>().detectCollisions = false;
    }
}
