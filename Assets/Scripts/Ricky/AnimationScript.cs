﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private PlayerControl controller;
    private CharacterController ch_controller;
    private Animator ch_anim;
    public GameObject bt;

    public bool itsounds;
    float ch_speedXZ;
    // Start is called before the first frame update
    void Start()
    {
        itsounds = false;
        ch_controller = GetComponent<CharacterController>();
        controller = GetComponent<PlayerControl>();
        ch_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.box != null)
        {
            if (controller.inFrontBox && !controller.box.GetComponent<BoxControl>().playerSide.Equals("")) //Dentro del trigger + delante de la caja
            {
                if (!controller.grabbingBox)
                {
                    if (Input.GetButtonDown("Fire3"))
                    {
                        if (ch_anim.GetBool("preparingPushAway"))
                        {
                            bt.SetActive(false);
                            ch_anim.SetBool("preparingPushAway", false);
                        }
                        else
                        {
                            bt.SetActive(true);
                            ch_anim.SetBool("preparingPushAway", true);
                        }
                    }
                    else if (Input.GetButtonUp("Fire3"))
                    {
                        bt.SetActive(false);
                        ch_anim.SetBool("preparingPushAway", false);
                    }
                }
            }
        }    
    
        ch_anim.SetBool("pushing", controller.grabbingBox);
        ch_speedXZ = Mathf.Abs(ch_controller.velocity.x) + Mathf.Abs(ch_controller.velocity.z);
        ch_anim.SetFloat("speed", controller.enabled ? ch_speedXZ : 0f);
        ch_anim.SetBool("joyinput", ch_controller.velocity.magnitude > 0.1f? Input.GetButton("L3") : false);

        if (ch_speedXZ > 0.1 && !itsounds)
        {
            if(AudioController.AudioInstance) AudioController.AudioInstance.soundPlayerSteps(true);
            itsounds = true;
        }
        else if (ch_speedXZ < 0.1 && itsounds) {
            if (AudioController.AudioInstance) AudioController.AudioInstance.soundPlayerSteps(false);
            itsounds = false;
        }

    }
}
