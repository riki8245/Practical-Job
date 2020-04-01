using System;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    CharacterController b_Controller;
    Vector3 b_moveDirection;
    PlayerController playerController;

    private float b_horizontalMove;
    private const float b_Speed = 5f;
    private const float b_gravity = 10f;
    private const float b_timeToRotate = .5f;
    private float b_verticalMove;
    private float b_fallVelocity;
    private Vector3 b_input;
    private string p_RelativePos;
    private float b_timePushed;
    bool b_AutoMove;
    private bool b_playerIsMoving;
    private float b_SpeedOverTime;

    private void Awake()
    {
        b_Controller = this.GetComponent<CharacterController>();
    }

    private void Start()
    {
        p_RelativePos = "";
        b_timePushed = 0f;
        b_AutoMove = false;
        b_SpeedOverTime = 0f;
    }
    public void RotateBox(float slopeAngle, float orientation)
    {
        if (orientation == 1f)
            iTween.RotateTo(this.gameObject, new Vector3(slopeAngle, 0f, 0f), b_timeToRotate);
        else if (orientation == 0f)
            iTween.RotateTo(this.gameObject, new Vector3(-slopeAngle, 0f, 0f), b_timeToRotate);
        else if (orientation < 0f)
            iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, -slopeAngle), b_timeToRotate);
        else if (orientation > 0f && orientation < 1f)
            iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, slopeAngle), b_timeToRotate);
        else
            iTween.RotateTo(this.gameObject, Vector3.up, 1.5f);

    }

    private void Update()
    {
        b_horizontalMove = Input.GetAxis("Horizontal");
        b_verticalMove = Input.GetAxis("Vertical");
        MoveBox();
        if (b_AutoMove)
        {
            b_moveDirection.x = (b_moveDirection.x > 0f) ? Mathf.Clamp(b_moveDirection.x * (b_timePushed / b_SpeedOverTime),0f,b_Speed): (b_moveDirection.x < 0f) ? Mathf.Clamp(b_moveDirection.x * b_timePushed / b_SpeedOverTime, -b_Speed, 0f) : b_moveDirection.x ;
            b_moveDirection.z = (b_moveDirection.z > 0f) ? Mathf.Clamp(b_moveDirection.z * (b_timePushed / b_SpeedOverTime), 0f, b_Speed) : (b_moveDirection.z < 0f) ? Mathf.Clamp(b_moveDirection.z * b_timePushed / b_SpeedOverTime, -b_Speed, 0f) : b_moveDirection.z;

            //b_moveDirection *= Mathf.Clamp(b_Speed * b_timePushed, 0, 1f);
            b_timePushed -= Time.deltaTime * 0.01f;
            if (b_timePushed <= 0f || (b_moveDirection.x ==0 && b_moveDirection.z == 0))
            {
                b_AutoMove = false;
                b_moveDirection = Vector3.zero;
                b_timePushed = 0f;
                b_SpeedOverTime = 0f;
            }
                
        }
        SetGravity();
        b_Controller.Move(b_moveDirection * Time.deltaTime);
    }

    private void SetGravity()
    {
        if (b_Controller.isGrounded)
            b_fallVelocity = -b_gravity * Time.deltaTime;

        else
            b_fallVelocity -= b_gravity * Time.deltaTime;

        b_moveDirection.y = b_fallVelocity;

    }

    private void MoveBox()
    {
        b_input = new Vector3(b_horizontalMove, 0f, b_verticalMove);
        b_input = b_input.normalized;
        b_input = Camera.main.transform.TransformDirection(b_input);
        b_input.y = 0.0f;

        if (playerController != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float p_relativeXPos = playerController.transform.position.x - transform.position.x;
                float p_relativeZPos = playerController.transform.position.z - transform.position.z;
                if (p_relativeXPos <= .5f && Mathf.Abs(p_relativeZPos) <= .5f)
                    p_RelativePos = "LeftSide";
                else if (p_relativeXPos >= .5f && Mathf.Abs(p_relativeZPos) <= .5f)
                    p_RelativePos = "RightSide";
                else if (p_relativeZPos <= .5f && Mathf.Abs(p_relativeXPos) <= .5f)
                    p_RelativePos = "DownSide";
                else if (p_relativeZPos >= .5f && Mathf.Abs(p_relativeXPos) <= .5f)
                    p_RelativePos = "UpSide";

                b_timePushed = 0f;
                playerController.axisToUseWhileBox = (p_RelativePos.Equals("LeftSide") || p_RelativePos.Equals("RightSide") ? 1 : p_RelativePos.Equals("UpSide") || p_RelativePos.Equals("DownSide") ? 2 : 0);
                b_playerIsMoving = true;
            }
            if (Input.GetButton("Fire1") && b_playerIsMoving)
            {
                playerController.p_PushingOrPulling = true;
                if (b_input == Vector3.zero)
                    b_timePushed = 0f;
                playerController.p_timeMovingObject = b_timePushed;
                b_moveDirection = b_input * Mathf.Clamp(b_Speed * b_timePushed,.1f,b_Speed);

                if (playerController.axisToUseWhileBox == 1)
                    b_moveDirection.z = 0f;
                else if (playerController.axisToUseWhileBox == 2)
                    b_moveDirection.x = 0f;
                else
                    b_moveDirection = Vector3.zero;
                if (b_moveDirection != Vector3.zero)
                    b_timePushed += Time.deltaTime * 0.5f;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                playerController.p_PushingOrPulling = false;
                playerController.axisToUseWhileBox = 0;
                b_playerIsMoving = false;
                b_SpeedOverTime = b_timePushed;
                switch (p_RelativePos)
                {
                    case "LeftSide":
                        if (b_moveDirection.x < 0f)
                            b_moveDirection = Vector3.zero;
                        else if (b_Controller.velocity.magnitude > 3.5f)
                            b_AutoMove = true;
                        else
                            b_moveDirection = Vector3.zero;
                        break;
                    case "RightSide":
                        if (b_moveDirection.x > 0f)
                            b_moveDirection = Vector3.zero;
                        else if(b_Controller.velocity.magnitude >3.5f)
                            b_AutoMove = true;
                        else
                            b_moveDirection = Vector3.zero;
                        break;
                    case "DownSide":
                        if (b_moveDirection.z < 0f)
                            b_moveDirection = Vector3.zero;
                        else if (b_Controller.velocity.magnitude > 3.5f)
                            b_AutoMove = true;
                        else
                            b_moveDirection = Vector3.zero;
                        break;
                    case "UpSide":
                        if (b_moveDirection.z > 0f)
                            b_moveDirection = Vector3.zero;
                        else if (b_Controller.velocity.magnitude > 3.5f)
                            b_AutoMove = true;
                        else
                            b_moveDirection = Vector3.zero;
                        break;
                    default:
                        b_AutoMove = false;
                        break;

                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.p_PushingOrPulling = false;
            playerController = null;
        }
    }
    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        
        GUI.Label(new Rect(10, 130, 500, 100), "B_Velocity: " + b_moveDirection, guiStyle);
        GUI.Label(new Rect(10, 200, 500, 100), "B_VelocityMag: " + b_Controller.velocity.magnitude, guiStyle);
        GUI.Label(new Rect(10, 320, 500, 100), "b_AutoMove: " + (b_AutoMove), guiStyle);




    }
}
