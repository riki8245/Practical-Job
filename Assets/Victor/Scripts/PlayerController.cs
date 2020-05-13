using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int FaceState;
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
    [SerializeField] private GameObject p_Head;
    [SerializeField] private Material neutral, scary, vulnerable;
    public Material[] mats;

    public Vector3 p_moveDirection;
    private Vector3 p_input;
    

    private void Awake()
    {
        //p_animator = this.GetComponent<Animator>();
        p_Speed = 4.5f;
        p_gravity = 35f;
        p_moveDirection = Vector3.zero;
        
    }
    void Start()
    {
        p_Controller = this.GetComponent<CharacterController>();
        mats = p_Head.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = neutral;
        FaceState = 0;
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
        ChangeFaceTexture();
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

    private void ChangeFaceTexture()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            ChangeFaceState();
            switch (mats[1].name.ToString()){
                case "Face_neutral": mats[1] = vulnerable; break;
                case "Face_scary": mats[1] = neutral; break;
                case "Face_vulnerable": mats[1] = scary; break;
                default: break;
            }
            print(mats[1].name);
            p_Head.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }

    void ChangeFaceState()
    {
        if (FaceState == 2) FaceState = 0;
        else FaceState++;
    }

    private void MovePlayer()
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;
        //p_moveDirection = p_input.x * cameraAdjust.camRight + p_input.z * cameraAdjust.camForward;
        p_input = Camera.main.transform.TransformDirection(p_input);
        p_input.y = 0.0f;
        p_moveDirection = p_input;

        p_Speed = Input.GetButton("L3") ? 6.5f : 4.5f;

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

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        //GUI.Label(new Rect(10, 90, 500, 100), "p_PushingOrPulling: " + p_PushingOrPulling, guiStyle);
    }
}
