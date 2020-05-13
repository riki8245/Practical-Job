using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private PlayerController controller;
    private CharacterController ch_controller;
    private Animator ch_anim;
    // Start is called before the first frame update
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        controller = GetComponent<PlayerController>();
        ch_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ch_anim.SetBool("pushing", controller.p_PushingOrPulling);
        ch_anim.SetFloat("speed", controller.enabled ? ch_controller.velocity.magnitude : 0f);
        ch_anim.SetBool("joyinput", ch_controller.velocity.magnitude > 0.1f? Input.GetButton("L3") : false);
    }
}
