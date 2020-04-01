using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest1 : MonoBehaviour
{
    private BoxCollider[] b_Colliders;
    private bool isGrounded;

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
        //other.gameObject.GetComponent<CharacterController>().attachedRigidbody.detectCollisions = false;
        isGrounded = other.GetComponent<CharacterController>().isGrounded;
        int F = LayerMask.GetMask("Floor");
        int B = LayerMask.GetMask("Boxes");
        print(F + B);
        Physics.IgnoreLayerCollision(8,9 ,true);
        //other.GetComponent<CharacterController>().attachedRigidbody;

    }
    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        GUI.Label(new Rect(10, 400, 500, 100), "BoxGrounded + portal : " + isGrounded, guiStyle);

    }
}
