﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float p_horizontalMove;
    float p_verticalMove;
    float p_timeMovingObject;
    float p_fallVelocity;

    public int axisToUseWhileBox = 0;

    public float p_gravity;
    public float p_Speed;

    public bool p_PushingOrPulling;
    public bool p_CanMoveBox;


    CharacterController p_Controller;
    Camera mainCamera;
    RaycastHit hit;
    Animator p_animator;

    public Vector3 p_moveDirection;
    private Vector3 p_input;
    private Vector3 camForward;
    private Vector3 camRight;

    private void Awake()
    {
        p_Controller = this.GetComponent<CharacterController>();
        //p_animator = this.GetComponent<Animator>();
        p_Speed = 3.5f;
        p_gravity = 35f;
        p_moveDirection = Vector3.zero;
        mainCamera = Camera.main;
    }
    void Start()
    {
        p_CanMoveBox = false;
        p_horizontalMove = 0f;
        p_verticalMove = 0f;
        p_timeMovingObject = 0f;
        CamDirection();

    }

    // Update is called once per frame
    void Update()
    {
        p_horizontalMove = Input.GetAxis("Horizontal");
        p_verticalMove = Input.GetAxis("Vertical");
        CheckIfCanMoveBox();
        MovePlayer();
    }

    private void CheckIfCanMoveBox()
    {
        //Ray Cast which provides info of what it is in front player
        int layerMask = LayerMask.GetMask("Boxes");
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.yellow);
        if (Physics.Raycast(transform.position, transform.forward, out hit, .1f, layerMask))
        {
            p_CanMoveBox = (Vector3.Angle(hit.normal, -transform.forward)) <= 45f ? true : false;
        }
        else
            p_CanMoveBox = false;
                

    }

    private void MovePlayer()
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;


        p_moveDirection = p_input.x * camRight + p_input.z * camForward;
        p_moveDirection *= p_Speed;

        if (p_moveDirection != Vector3.zero && !p_PushingOrPulling)
            p_Controller.transform.LookAt(p_Controller.transform.position + p_moveDirection);
        else if(p_PushingOrPulling)
            MovingObject();

        SetGravity();
        p_Controller.Move(p_moveDirection * Time.deltaTime);
        //p_animator.SetFloat("velocity", Mathf.Abs(p_horizontalMove) + Mathf.Abs(p_verticalMove));
        

    }

    private void SetGravity()
    {
        if (p_Controller.isGrounded)
        {
            p_fallVelocity = -p_gravity * Time.deltaTime;
            p_moveDirection.y = p_fallVelocity;
        }

        else
        {
            p_fallVelocity -= p_gravity * Time.deltaTime;
            p_moveDirection.y = p_fallVelocity;
        }
    }

    private void CamDirection() // Direction where camera looks
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized; ;
    }

    private void MovingObject()
    {
        if (p_PushingOrPulling)
        {
            if (axisToUseWhileBox == 1)
                p_moveDirection = Vector3.right * (p_horizontalMove > 0f ? 1 : p_horizontalMove < 0f? -1 :  0) * p_Speed;
            else
                p_moveDirection = Vector3.forward * (p_verticalMove > 0f ? 1 : p_verticalMove < 0f ? -1 : 0) * p_Speed;

        }
    }
    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        //GUI.Label(new Rect(10, 100, 500, 100), "Direccion jugador" + transform.forward, guiStyle);
        //GUI.Label(new Rect(10, 150, 500, 100), "Direccion cámara" + camForward, guiStyle);
        GUI.Label(new Rect(10, 200, 500, 100), "Pushing" + p_PushingOrPulling, guiStyle);



    }
}
