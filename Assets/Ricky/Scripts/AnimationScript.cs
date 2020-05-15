using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private PlayerControl controller;
    private CharacterController ch_controller;
    private Animator ch_anim;
    [SerializeField] private GameObject bt;
    // Start is called before the first frame update
    void Start()
    {
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
                        bt.SetActive(true);
                        ch_anim.SetBool("preparingPushAway", true);
                    }
                    else if (Input.GetButtonUp("Fire3"))
                    {
                        bt.SetActive(false);
                        ch_anim.SetTrigger("pushAway");
                        ch_anim.SetBool("preparingPushAway", false);
                    }
                }
            }
        }    
    
        ch_anim.SetBool("pushing", controller.grabbingBox);
        ch_anim.SetFloat("speed", controller.enabled ? ch_controller.velocity.magnitude : 0f);
        ch_anim.SetBool("joyinput", ch_controller.velocity.magnitude > 0.1f? Input.GetButton("L3") : false);
    }
}
