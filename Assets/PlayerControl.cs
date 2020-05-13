using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]private Vector3 p_input;
    [SerializeField] private float p_Speed;
    [SerializeField] private bool pushingOrPulling;
    [SerializeField] private float p_timeMovingObject;
    [SerializeField] private int axisToUseWhileBox;
    [SerializeField] private float p_fallVelocity;
    [SerializeField] private const int p_gravity = 45;
    public GameObject box;

    private void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        pushingOrPulling = false;
        box = null;
    }
    private void Update()
    {
        float p_horizontalMove = Input.GetAxis("Horizontal");
        float p_verticalMove = Input.GetAxis("Vertical");
        MovePlayer(p_horizontalMove , p_verticalMove);
    }

    private void MovePlayer(float p_horizontalMove, float p_verticalMove)
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;
        p_input = Camera.main.transform.TransformDirection(p_input);
        p_input.y = 0.0f;

        p_Speed = Input.GetButton("L3") ? 6.5f : 4.5f;
        ManageInputs();
        if (p_input != Vector3.zero && !pushingOrPulling)
        {
            characterController.transform.LookAt(characterController.transform.position + p_input);
            p_input *= p_Speed;
        }

        else if (pushingOrPulling)
        {
            if (axisToUseWhileBox == 1)
                p_input.z = 0f;
            else if (axisToUseWhileBox == 2)
                p_input.x = 0f;
            p_input *= Mathf.Clamp(p_Speed, .1f, p_Speed);
        }


        SetGravity();
        characterController.Move(p_input * Time.deltaTime);
    }
    void ManageInputs()
    {
        if (box != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                String p_RelativePos = "";
                float p_relativeXPos = this.transform.position.x - box.transform.position.x;
                float p_relativeZPos = this.transform.position.z - box.transform.position.z;
                if (p_relativeXPos <= .5f && Mathf.Abs(p_relativeZPos) <= .5f)
                    p_RelativePos = "LeftSide";
                else if (p_relativeXPos >= .5f && Mathf.Abs(p_relativeZPos) <= .5f)
                    p_RelativePos = "RightSide";
                else if (p_relativeZPos <= .5f && Mathf.Abs(p_relativeXPos) <= .5f)
                    p_RelativePos = "DownSide";
                else if (p_relativeZPos >= .5f && Mathf.Abs(p_relativeXPos) <= .5f)
                    p_RelativePos = "UpSide";

                axisToUseWhileBox = (p_RelativePos.Equals("LeftSide") || p_RelativePos.Equals("RightSide") ? 1 : p_RelativePos.Equals("UpSide") || p_RelativePos.Equals("DownSide") ? 2 : 0);
                box.transform.parent = this.transform;
                pushingOrPulling = true;
                characterController.radius = 1.3f;
                characterController.center = new Vector3(0f,.3f,.65f);
            }
            if (Input.GetButton("Fire1"))
            {
                
                pushingOrPulling = true;
                print("entra");

            }
            else if(Input.GetButtonUp("Fire1"))
            {
                characterController.center = new Vector3(0f, 0f, 0f);
                characterController.radius = .5f;
                box.GetComponent<BoxControl>().Move();
                box.transform.parent = null;
                pushingOrPulling = false;
            }
        }   
    }
    private void SetGravity()
    {
        if (characterController.isGrounded)
        {
            p_fallVelocity = -p_gravity * Time.deltaTime;
            p_input.y = p_fallVelocity;
        }

        else
        {
            p_fallVelocity -= p_gravity * Time.deltaTime;
            p_input.y = p_fallVelocity;
        }
    }
}
