using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    public pressurePlateController[] pressurePlates;
    public GameObject door;
    private Animator door_anim;
    bool itsounds;
    // Start is called before the first frame update
    void Start()
    {
        door_anim = door.GetComponentInChildren<Animator>();
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
                door_anim.SetBool("open", true);
                if (!itsounds)
                {
                    AudioController.AudioInstance.soundDoor(true);
                    itsounds = true;
                }
                door.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                if (itsounds)
                {
                    AudioController.AudioInstance.soundDoor(false);
                    itsounds = false;
                }
                door_anim.SetBool("open", false);
                door.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }
}
