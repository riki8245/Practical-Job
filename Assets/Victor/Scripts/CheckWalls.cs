using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnColiison(Collider other)
    {
        print("colisiona");
    }
    private void OnTriggerExit(Collider other)
    {
        print("Sale");
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("colisiona");
    }
    private void OnCollisionExit(Collision collision)
    {
        print("Sale");
    }
}
