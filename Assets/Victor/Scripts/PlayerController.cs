using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float p_horizontalMove;
    float p_verticalMove;
    float p_fallVelocity;

    public int axisToUseWhileBox = 0;

    public float p_timeMovingObject;
    public float p_gravity;
    public float p_Speed;

    public bool p_PushingOrPulling;
    public bool p_CanMoveBox;


    CharacterController p_Controller;
    RaycastHit hit;
    Animator p_animator;
    public CameraAdjust cameraAdjust;

    public Vector3 p_moveDirection;
    private Vector3 p_input;
    

    private void Awake()
    {
        //p_animator = this.GetComponent<Animator>();
        p_Speed = 5f;
        p_gravity = 35f;
        p_moveDirection = Vector3.zero;
        
    }
    void Start()
    {
        p_Controller = this.GetComponent<CharacterController>();
        p_CanMoveBox = false;
        p_horizontalMove = 0f;
        p_verticalMove = 0f;
        p_timeMovingObject = 0f;

    }
    void Update()
    {
        p_horizontalMove = Input.GetAxis("Horizontal");
        p_verticalMove = Input.GetAxis("Vertical");
        //CheckIfCanMoveBox();
        MovePlayer();
    }

    //private void CheckIfCanMoveBox()
    //{
    //    //Ray Cast which provides info of what it is in front player
    //    int layerMask = LayerMask.GetMask("Boxes");
    //    //Debug.DrawRay(transform.position, transform.forward * 1000, Color.yellow);
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, .1f, layerMask))
    //    {
    //        p_CanMoveBox = (Vector3.Angle(hit.normal, -transform.forward)) <= 10f ? true : false;
    //    }
       
                

    //}

    private void MovePlayer()
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;
        //p_moveDirection = p_input.x * cameraAdjust.camRight + p_input.z * cameraAdjust.camForward;
        p_input = Camera.main.transform.TransformDirection(p_input);
        p_input.y = 0.0f;
        p_moveDirection = p_input;

        if (p_moveDirection != Vector3.zero && !p_PushingOrPulling)
        {
            p_Controller.transform.LookAt(p_Controller.transform.position + p_moveDirection);
            p_moveDirection *= p_Speed;
        }
           
        else if (p_PushingOrPulling)
        {
            if (axisToUseWhileBox == 1)
                p_moveDirection.z = 0f;
            else if (axisToUseWhileBox == 2)
                p_moveDirection.x = 0f;
            p_moveDirection *= Mathf.Clamp(p_timeMovingObject * p_Speed, .1f, p_Speed);
        }          


        //SetGravity();
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

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        //GUI.Label(new Rect(10, 90, 500, 100), "p_PushingOrPulling: " + p_PushingOrPulling, guiStyle);
    }
}
