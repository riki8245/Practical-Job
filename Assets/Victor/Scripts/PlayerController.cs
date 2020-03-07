using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float p_Speed;
    [SerializeField] float p_Gravity;
    [SerializeField] float p_AxisXMove;
    [SerializeField] float p_AxisZMove;
    [SerializeField] float p_fallVelocity;


    public bool p_CanGrabObject;
    public bool p_PushingOrPulling;
    public Vector3 b_ForceDirection;
    public Vector3 p_PositionOffset;

    CharacterController p_Controller;

    public Vector3 p_Move;
    public bool p_AxisXMoveEnable;
    public bool p_AxisZMoveEnable;

    private void Awake()
    {
        p_Controller = this.GetComponent<CharacterController>();
        p_Speed = 3.5f;
        p_Gravity = 1f;
        p_AxisXMove = 0f;
        p_AxisZMove = 0f;
        p_Move = new Vector3();
        b_ForceDirection = new Vector3();
        p_PositionOffset = new Vector3();
        p_AxisXMoveEnable = true;
        p_AxisZMoveEnable = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        p_AxisXMove = 0f;
        p_AxisZMove = 0f;
        if (p_AxisXMoveEnable) { p_AxisXMove = Input.GetAxisRaw("Horizontal"); }
        if (p_AxisZMoveEnable) { p_AxisZMove = Input.GetAxisRaw("Vertical"); }
        
        CheckIfMovableBox();
        MovePlayer();        

    }

    private void CheckIfMovableBox()
    {
      
        //Ray Cast

        int layerMask = LayerMask.GetMask("Boxes");       
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, .5f, layerMask) &&
            GetCharacterDirectionFace())
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Box Hit");
            p_CanGrabObject = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Box not Hit");
            p_CanGrabObject = false;

        }
    }

    private void MovePlayer()
    {
        p_Move = new Vector3(p_AxisXMove, 0f, p_AxisZMove);
        p_Move = p_Move.normalized * p_Speed * Time.deltaTime;
        if (p_Move != Vector3.zero && !p_PushingOrPulling)
        {
            transform.rotation = Quaternion.LookRotation(p_Move);
        }
        SetGravity();

        p_Controller.Move(p_Move);
    }

    private void SetGravity()
    {
        if (p_Controller.isGrounded)
        {
            p_fallVelocity = -p_Gravity * Time.deltaTime;
            p_Move.y = p_fallVelocity;
        }
        else
        {
            p_fallVelocity -= p_Gravity * Time.deltaTime;
            p_Move.y = p_fallVelocity;
        }
    }

    private void LateUpdate()
    {
        p_PositionOffset = transform.position;   
    }
    public bool GetCharacterDirectionFace()
    {
        Vector3 p_FaceDirection = transform.forward;
        p_FaceDirection.y = 0;
        p_FaceDirection.Normalize();


        if (Vector3.Angle(p_FaceDirection, Vector3.forward) <= 3f)
        {
            b_ForceDirection = new Vector3(0, 0, 1);
            return true;
        }
        if (Vector3.Angle(p_FaceDirection, Vector3.back) <= 3f)
        {
            b_ForceDirection = new Vector3(0, 0, -1);
            return true;
        }
        if (Vector3.Angle(p_FaceDirection, Vector3.right) <= 3f)
        {
            b_ForceDirection = new Vector3(1, 0, 0);
            return true;
        }
        if (Vector3.Angle(p_FaceDirection, Vector3.left) <= 3f)
        {
            b_ForceDirection = new Vector3(-1, 0, 0);
            return true;
        }

        b_ForceDirection = Vector3.zero;
        return false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * 2f;
    }

}
