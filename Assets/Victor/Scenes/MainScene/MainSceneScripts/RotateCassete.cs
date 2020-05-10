using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCassete : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20f * Time.deltaTime, 20f * Time.deltaTime, 20f * Time.deltaTime);
    }
}
