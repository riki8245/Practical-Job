using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region
    [SerializeField]
    float p_Speed;
    [SerializeField] float p_Gravity;
    [SerializeField] float p_AxisXMove;
    [SerializeField] float p_AxisZMove;
    [SerializeField] float p_fallVelocity;

    #endregion

    public bool p_PushingOrPulling;

    CharacterController p_Controller;

    public Vector3 p_Move;

    private void Awake()
    {
        p_Controller = this.GetComponent<CharacterController>();
        p_Speed = 4f;
        p_Gravity = 7f;
        p_AxisXMove = 0f;
        p_AxisZMove = 0f;
        p_Move = new Vector3();
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
        p_AxisXMove = Input.GetAxisRaw("Horizontal");
        p_AxisZMove = Input.GetAxisRaw("Vertical");

        MovePlayer();
    }

    private void MovePlayer()
    {
        p_Move = new Vector3(p_AxisXMove, 0f, p_AxisZMove);
        p_Move = p_Move.normalized * p_Speed * Time.deltaTime;
        if(p_Move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(p_Move);
        }
        SetGravity();
        PushingOrPulling();
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

    private void PushingOrPulling()
    {
        if (p_PushingOrPulling)
        {
            var v = transform.forward;
            v.y = 0;
            v.Normalize();

            if (Vector3.Angle(v, Vector3.forward) <= 45.0 || Vector3.Angle(v, Vector3.back) <= 45.0)
            {
                if (Vector3.Angle(v, Vector3.forward) <= 45.0)
                {
                    transform.forward = Vector3.forward;
                    Debug.Log("North");
                }
                else if (Vector3.Angle(v, Vector3.back) <= 45.0)
                {
                    transform.forward = Vector3.back;
                    Debug.Log("South");
                }
            }
            else
            {
                if (Vector3.Angle(v, Vector3.right) <= 45.0)
                {
                    transform.forward = Vector3.right;
                    Debug.Log("East");
                }
                else
                {
                    transform.forward = Vector3.left;
                    Debug.Log("West");
                }
            }
        }
    }
}
