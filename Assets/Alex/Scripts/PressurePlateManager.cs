using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    public pressurePlateController[] pressurePlates;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allOpen = true;
        for(int i = 0; i < pressurePlates.Length; i++)
        {
            allOpen = allOpen && pressurePlates[i].pressed;
        }
        if (!finishLevel.playerReachFinish)
        {
            if (allOpen)
            {
                door.SetBool("open", true);
            }
            else door.SetBool("open", false);
        }
    }
}
