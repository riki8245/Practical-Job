using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControllerV2 : MonoBehaviour
{

    CharacterController b_Controller;

    [SerializeField] Transform b_rayCastRight;
    [SerializeField] Transform b_rayCastLeft;
    [SerializeField] Transform b_rayCastFront;
    [SerializeField] Transform b_rayCastBack;
    RaycastHit b_hitFirstPoint;
    RaycastHit b_hitSecondPoint;

    Vector3 b_moveDirection;
    PlayerController playerController;



    private bool b_isGettingMoved;
    private bool b_firstTimeEnterSlope;
    private bool p_AlreadyPressed;
    private bool b_isGrounded;
    //bool b_grabableObject;


    private float b_horizontalMove;
    private float b_verticalMove;
    private float b_Speed;
    private float b_angleOfSlope;
    private float b_fallVelocity;
    private float b_gravity;

    private int layerMaskFloor;
    private bool b_playerStops;

    // Start is called before the first frame update

    private void Awake()
    {
        b_Controller = this.GetComponent<CharacterController>();
    }
    void Start()
    {
        //b_grabableObject = true;
        b_firstTimeEnterSlope = false;
        b_gravity = 3;
        b_Speed = 3.5f;
        layerMaskFloor = LayerMask.GetMask("Floor");
        b_moveDirection = Vector3.zero;
    }

    private void Update()
    {
        b_horizontalMove = Input.GetAxis("Horizontal");
        b_verticalMove = Input.GetAxis("Vertical");
        b_isGrounded = Physics.CheckSphere(this.transform.position - new Vector3(0f, 0.5f, 0f), .2f, layerMaskFloor);
        CheckSlope();
        MoveBox();
        SetGravity();
        b_Controller.Move(b_moveDirection * Time.deltaTime);
    }

    private void SetGravity()
    {
        //if (!b_isGrounded && transform.position.y >0.05f)
        //{
        //    b_fallVelocity -= b_gravity * Time.deltaTime;
        //    b_moveDirection.y = b_fallVelocity;
        //}
        //if (transform.position.y <= 0.05f)
        //    transform.position = new Vector3(transform.position.x,0f,transform.position.z);


        if (b_Controller.isGrounded)
        {
            b_fallVelocity = -b_gravity * Time.deltaTime;
            b_moveDirection.y = b_fallVelocity;
        }

        else
        {
            b_fallVelocity -= b_gravity * Time.deltaTime;
            b_moveDirection.y = b_fallVelocity;
        }
    }
    private void CheckSlope()
    {
        

        if (b_isGrounded && !b_firstTimeEnterSlope)
        {
            if(Physics.Raycast(b_rayCastRight.position, -transform.up * 10, out b_hitFirstPoint)&&
            Physics.Raycast(b_rayCastLeft.position, -transform.up * 10, out b_hitSecondPoint) &&
            (b_hitFirstPoint.normal != Vector3.up || b_hitSecondPoint.normal != Vector3.up) &&
            b_hitFirstPoint.normal != b_hitSecondPoint.normal)
            {
                if (b_hitFirstPoint.normal != Vector3.up)
                {
                    getAngle(b_hitFirstPoint.normal);
                    iTween.RotateTo(this.gameObject, new Vector3(0, 0, b_angleOfSlope), 1.5f);
                }
                else
                {
                    getAngle(b_hitSecondPoint.normal);
                    iTween.RotateTo(this.gameObject, new Vector3(0, 0, -b_angleOfSlope), 1.5f);
                }
                b_firstTimeEnterSlope = true;
            }
            else if ((Physics.Raycast(b_rayCastFront.position, -transform.up * 10, out b_hitFirstPoint) &&
                Physics.Raycast(b_rayCastBack.position, -transform.up * 10, out b_hitSecondPoint)) &&
                (b_hitFirstPoint.normal != Vector3.up || b_hitSecondPoint.normal != Vector3.up) &&
                b_hitFirstPoint.normal != b_hitSecondPoint.normal)
            {
                if (b_hitFirstPoint.normal != Vector3.up)
                {
                    getAngle(b_hitFirstPoint.normal);
                    iTween.RotateTo(this.gameObject, new Vector3(-b_angleOfSlope, 0f, 0f), 1.5f);
                }
                else
                {
                    getAngle(b_hitSecondPoint.normal);
                    iTween.RotateTo(this.gameObject, new Vector3(b_angleOfSlope, 0f, 0f), 1.5f);
                }
                b_firstTimeEnterSlope = true;
            }
        }
        if (b_hitFirstPoint.normal == b_hitSecondPoint.normal && b_hitSecondPoint.normal == Vector3.up)
            b_firstTimeEnterSlope = false;
        if (!b_isGrounded && b_firstTimeEnterSlope)
        {
            iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, 0f), 1f);
        }

    }
    private void getAngle(Vector3 angleToGet)
    {
        Vector3 vec1 = Vector3.up;
        Vector3 vec2 = angleToGet;
        float dot = Vector3.Dot(vec1, vec2);
        dot = dot / (vec1.magnitude * vec2.magnitude);
        float acos = Mathf.Acos(dot);
        b_angleOfSlope = acos * 180 / Mathf.PI;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerController = other.GetComponent<PlayerController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButton("Fire1"))
            b_isGettingMoved = true;
        if(Input.GetButtonUp("Fire1"))
        {
            b_playerStops = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b_playerStops = true;
            playerController = null;
        }
    }

    private void MoveBox()
    {
        
        if (playerController != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //playerController.transform.LookAt(this.transform.position);
                if (Mathf.Abs(playerController.transform.position.x - transform.position.x) >= .5f &&
                    Mathf.Abs(playerController.transform.position.z - transform.position.z) <= .5f)
                {
                    //if (!p_AlreadyPressed)
                    //{
                    //    if (playerController.transform.position.x > transform.position.x)
                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(.8f, 0, 0), .5f);

                    //    else
                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(-.8f, 0, 0), .5f);
                    //    p_AlreadyPressed = true;
                    //}
                    playerController.axisToUseWhileBox = 1;

                }
                else
                {
                    //if (!p_AlreadyPressed)
                    //{
                    //    if (playerController.transform.position.z > transform.position.z)
                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(0f, 0f, .8f), .5f);
                    //    else
                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(0f, 0f, -.8f), .5f);
                    //    p_AlreadyPressed = true;
                    //}

                    playerController.axisToUseWhileBox = 2;
                }

                playerController.p_PushingOrPulling = true;
            }

            
            if (b_isGettingMoved)
            {
                if (playerController.axisToUseWhileBox == 1)
                    b_moveDirection = Vector3.right * (b_horizontalMove > 0f ? 1 : b_horizontalMove < 0f ? -1 : 0) * b_Speed;
                else if (playerController.axisToUseWhileBox == 2)
                    b_moveDirection = Vector3.forward * (b_verticalMove > 0f ? 1 : b_verticalMove < 0f ? -1 : 0) * b_Speed;
            }
        }
        else
            b_moveDirection = Vector3.zero;

        if (b_playerStops)
        {
            playerController.p_PushingOrPulling = false;
            p_AlreadyPressed = false;
            playerController.axisToUseWhileBox = 0;
            b_isGettingMoved = false;
            b_playerStops = false;
        }


        //b_moveDirection = p_stopsPressing && b_moveDirection != Vector3.zero? Vector3.Lerp(b_moveDirection, Vector3.zero, 1) : b_moveDirection;





    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        GUI.Label(new Rect(10, 20, 500, 100), "isGrounded: " + b_isGrounded, guiStyle);
        GUI.Label(new Rect(10, 160, 500, 100), "b_isGettingMoved" + b_isGettingMoved, guiStyle);


        GUI.Label(new Rect(10, 300, 500, 100), "Controller" + (b_Controller.velocity), guiStyle);
        //GUI.Label(new Rect(10, 70, 500, 100), "Rotacion " + actual);
        //GUI.Label(new Rect(10, 100, 500, 100), "Desired " + target);






    }

}

   
