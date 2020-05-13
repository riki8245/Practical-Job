using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// //TAMPOCO VIO NUNCA LA LUZ :)
public class BoxControllerV2 : MonoBehaviour
{

        CharacterController b_Controller;

    private void Start()
    {
        

    }
    private void Update()
    {
        b_Controller = this.GetComponent<CharacterController>();
        b_Controller.Move(new Vector3(1, 0, 0) * Time.deltaTime);
    }

    //    [SerializeField] Transform b_rayCastRight;
    //    [SerializeField] Transform b_rayCastLeft;
    //    [SerializeField] Transform b_rayCastFront;
    //    [SerializeField] Transform b_rayCastBack;
    //    RaycastHit b_hitFirstPoint;
    //    RaycastHit b_hitSecondPoint;

    //    Vector3 b_moveDirection;
    //    PlayerController playerController;



    //    private bool b_isGettingMoved;
    //    private bool b_firstTimeEnterSlope;
    //    private bool p_AlreadyPressed;
    //    private bool b_isGrounded;
    //    //bool b_grabableObject;


    //    private float b_horizontalMove;
    //    private float b_verticalMove;
    //    private float b_Speed;
    //    private float b_angleOfSlope;
    //    private float b_fallVelocity;
    //    private float b_gravity;

    //    private int layerMaskFloor;
    //    private bool b_playerStops;
    //    private bool b_firstTimeExitSlope;

    //    // Start is called before the first frame update

    //    private void Awake()
    //    {
    //        b_Controller = this.GetComponent<CharacterController>();
    //    }
    //    void Start()
    //    {
    //        //b_grabableObject = true;
    //        b_firstTimeEnterSlope = false;
    //        b_firstTimeExitSlope = false;
    //        b_gravity = 3;
    //        b_Speed = 3.5f;
    //        b_moveDirection = Vector3.zero;
    //    }



    //    private void Update()
    //    {
    //        b_horizontalMove = Input.GetAxis("Horizontal");
    //        b_verticalMove = Input.GetAxis("Vertical");
    //        CheckSlope();
    //        MoveBox();
    //        SetGravity();
    //        b_Controller.Move(b_moveDirection * Time.deltaTime);
    //    }

    //    private void SetGravity()
    //    {


    //        if (b_Controller.isGrounded)
    //        {
    //            b_fallVelocity = -b_gravity * Time.deltaTime;
    //            b_moveDirection.y = b_fallVelocity;
    //        }

    //        else
    //        {
    //            b_fallVelocity -= b_gravity * Time.deltaTime;
    //            b_moveDirection.y = b_fallVelocity;
    //        }
    //    }
    //    private void CheckSlope()
    //    {
    //        if (b_isGrounded && !b_firstTimeEnterSlope)
    //        {
    //          if (Physics.Raycast(b_rayCastRight.position, -transform.up * 10, out b_hitFirstPoint) &&
    //                Physics.Raycast(b_rayCastLeft.position, -transform.up * 10, out b_hitSecondPoint) &&
    //                (b_hitFirstPoint.normal != Vector3.up || b_hitSecondPoint.normal != Vector3.up) &&
    //                b_hitFirstPoint.normal != b_hitSecondPoint.normal)
    //            {
    //                if (b_hitFirstPoint.normal != Vector3.up)
    //                {
    //                    getAngle(b_hitFirstPoint.normal);
    //                    iTween.RotateTo(this.gameObject, new Vector3(0, 0, b_angleOfSlope), 1.5f);
    //                }
    //                else
    //                {
    //                    getAngle(b_hitSecondPoint.normal);
    //                    iTween.RotateTo(this.gameObject, new Vector3(0, 0, -b_angleOfSlope), 1.5f);
    //                }
    //                b_firstTimeEnterSlope = true;
    //            }
    //            else if (Physics.Raycast(b_rayCastFront.position, -transform.up * 10, out b_hitFirstPoint) &&
    //                Physics.Raycast(b_rayCastBack.position, -transform.up * 10, out b_hitSecondPoint) &&
    //                (b_hitFirstPoint.normal != Vector3.up || b_hitSecondPoint.normal != Vector3.up) &&
    //                b_hitFirstPoint.normal != b_hitSecondPoint.normal)
    //            {
    //                if (b_hitFirstPoint.normal != Vector3.up)
    //                {
    //                    getAngle(b_hitFirstPoint.normal);
    //                    iTween.RotateTo(this.gameObject, new Vector3(-b_angleOfSlope, 0f, 0f), 1.5f);
    //                }
    //                else
    //                {
    //                    getAngle(b_hitSecondPoint.normal);
    //                    iTween.RotateTo(this.gameObject, new Vector3(b_angleOfSlope, 0f, 0f), 1.5f);
    //                }
    //                b_firstTimeEnterSlope = true;
    //            }
    //            if (b_firstTimeEnterSlope)
    //                StartCoroutine(ExecuteAfterTime(1.5f));
    //            else if(b_firstTimeExitSlope)
    //            {
    //                if (Physics.Raycast(b_rayCastRight.position, -transform.up * 10, out b_hitFirstPoint) &&
    //                    Physics.Raycast(b_rayCastLeft.position, -transform.up * 10, out b_hitSecondPoint) &&
    //                    (b_hitFirstPoint.normal == Vector3.up || b_hitSecondPoint.normal == Vector3.up) &&
    //                    b_hitFirstPoint.normal != b_hitSecondPoint.normal)
    //                {
    //                    if (b_hitFirstPoint.normal == Vector3.up)
    //                    {
    //                        getAngle(b_hitFirstPoint.normal);
    //                        iTween.RotateTo(this.gameObject, new Vector3(0, 0, b_angleOfSlope), 1.5f);
    //                    }
    //                    else
    //                    {
    //                        getAngle(b_hitSecondPoint.normal);
    //                        iTween.RotateTo(this.gameObject, new Vector3(0, 0, -b_angleOfSlope), 1.5f);
    //                    }
    //                    b_firstTimeExitSlope = false;
    //                }
    //                else if (Physics.Raycast(b_rayCastFront.position, -transform.up * 10, out b_hitFirstPoint) &&
    //                    Physics.Raycast(b_rayCastBack.position, -transform.up * 10, out b_hitSecondPoint) &&
    //                    (b_hitFirstPoint.normal == Vector3.up || b_hitSecondPoint.normal == Vector3.up) &&
    //                    b_hitFirstPoint.normal != b_hitSecondPoint.normal)
    //                {
    //                    if (b_hitFirstPoint.normal == Vector3.up)
    //                    {
    //                        getAngle(b_hitFirstPoint.normal);
    //                        iTween.RotateTo(this.gameObject, new Vector3(-b_angleOfSlope, 0f, 0f), 1.5f);
    //                    }
    //                    else
    //                    {
    //                        getAngle(b_hitSecondPoint.normal);
    //                        iTween.RotateTo(this.gameObject, new Vector3(b_angleOfSlope, 0f, 0f), 1.5f);
    //                    }
    //                    b_firstTimeExitSlope = false;

    //                }
    //            }

    //            if (b_hitFirstPoint.normal == b_hitSecondPoint.normal && b_hitFirstPoint.normal == Vector3.up)
    //                b_firstTimeEnterSlope = false;
    //        }
    //        if (!b_isGrounded && b_firstTimeEnterSlope)
    //            iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, 0f), 1f);


    //    }
    //    private void getAngle(Vector3 angleToGet)
    //    {
    //        Vector3 vec1 = Vector3.up;
    //        Vector3 vec2 = angleToGet;
    //        float dot = Vector3.Dot(vec1, vec2);
    //        dot = dot / (vec1.magnitude * vec2.magnitude);
    //        float acos = Mathf.Acos(dot);
    //        b_angleOfSlope = acos * 180 / Mathf.PI;
    //    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.CompareTag("Player"))
    //            playerController = other.GetComponent<PlayerController>();
    //    }
    //    private void OnTriggerStay(Collider other)
    //    {
    //        if (other.CompareTag("Player") && Input.GetButton("Fire1"))
    //            b_isGettingMoved = true;
    //        else
    //            b_playerStops = true;
    //    }
    //    private void OnTriggerExit(Collider other)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //            b_playerStops = true;
    //            playerController = null;
    //        }
    //    }

    //    private void MoveBox()
    //    {

    //        if (playerController != null)
    //        {
    //            if (Input.GetButtonDown("Fire1"))
    //            {
    //                //playerController.transform.LookAt(this.transform.position);
    //                if (Mathf.Abs(playerController.transform.position.x - transform.position.x) >= .5f &&
    //                    Mathf.Abs(playerController.transform.position.z - transform.position.z) <= .5f)
    //                {
    //                    #region
    //                    //if (!p_AlreadyPressed)
    //                    //{
    //                    //    if (playerController.transform.position.x > transform.position.x)
    //                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(.8f, 0, 0), .5f);

    //                    //    else
    //                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(-.8f, 0, 0), .5f);
    //                    //    p_AlreadyPressed = true;
    //                    //}
    //                    #endregion
    //                    playerController.axisToUseWhileBox = 1;

    //                }
    //                else
    //                {
    //                    #region
    //                    //if (!p_AlreadyPressed)
    //                    //{
    //                    //    if (playerController.transform.position.z > transform.position.z)
    //                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(0f, 0f, .8f), .5f);
    //                    //    else
    //                    //        iTween.MoveTo(playerController.gameObject, transform.position + new Vector3(0f, 0f, -.8f), .5f);
    //                    //    p_AlreadyPressed = true;
    //                    //}
    //                    #endregion

    //                    playerController.axisToUseWhileBox = 2;
    //                }

    //                playerController.p_PushingOrPulling = true;
    //            }


    //            if (b_isGettingMoved && !b_playerStops)
    //            {
    //                if (playerController.axisToUseWhileBox == 1)
    //                    b_moveDirection = Vector3.right * (b_horizontalMove > 0f ? 1 : b_horizontalMove < 0f ? -1 : 0) * b_Speed;
    //                else if (playerController.axisToUseWhileBox == 2)
    //                    b_moveDirection = Vector3.forward * (b_verticalMove > 0f ? 1 : b_verticalMove < 0f ? -1 : 0) * b_Speed;
    //            }
    //            else if (b_playerStops)
    //            {
    //                playerController.p_PushingOrPulling = false;
    //                playerController.axisToUseWhileBox = 0;
    //                b_playerStops = false;
    //                //ApplyVelocity();
    //            }
    //        }
    //        else
    //            b_moveDirection = Vector3.zero;

    //    }

    //    private void ApplyVelocity()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerator ExecuteAfterTime(float time)
    //    {
    //        yield return new WaitForSeconds(time);
    //        Debug.Log("Entra XD");
    //        b_firstTimeExitSlope = true;
    //    }


}

   
