using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadToPlayAnimations : MonoBehaviour
{
    Animator ch_anim;
    float speed;
    void Start()
    {
        ch_anim = GetComponent<Animator>();

        //Push_Box
        ch_anim.SetBool("pushing", true);  //To play
        ch_anim.SetBool("pushing", false); //To stop 

        //walk ( this usually goes in the Update() )
        ch_anim.SetFloat("speed", speed);  //where speed is the float that controls the velocity of the Character 
                                           //if speed > 0.1 it walks, if speed < 0.1 it goes to idle

        //Norauto (only playable if the character is walking, speed > 0.1)
        ch_anim.SetBool("joyinput", true);  //To play 
        ch_anim.SetBool("joyinput", false); //To stop
                                            //if the L3 button is pressed (the Left joystick button)
    }

}
