using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControllerV2 : MonoBehaviour
{

    CharacterController b_Controller;
    Vector3 b_Movement;

    [SerializeField] float b_fallVelocity;
    [SerializeField] Transform b_rayCastRight;
    [SerializeField] Transform b_rayCastLeft;
    [SerializeField] Transform b_rayCastFront;
    [SerializeField] Transform b_rayCastBack;
    [Range(0.0f, 10.0f)] public float b_speed;


    RaycastHit b_hitFirstPoint;
    RaycastHit b_hitSecondPoint;

    Vector3 b_desiredRotation;

    [SerializeField] bool b_onSlope;
    private bool b_rotating;
    float timer = 0f;

    // Start is called before the first frame update

    private void Awake()
    {
        b_Controller = this.GetComponent<CharacterController>();
    }
    void Start()
    {
        b_onSlope = false;
    }

    // Update is called once per frame
    void Update()
    {
    
        RotationByPlane();
        /*if (b_rotating)
        {
            timer += Time.deltaTime;
            //this.transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(b_desiredRotation),timer);
            this.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal), 1f*Time.deltaTime);

        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.up), Quaternion.Euler(b_desiredRotation), Time.deltaTime * b_speed);

        }*/
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.up, Color.green);

        b_Movement = -transform.forward * 5f * Time.deltaTime;
      
        SetGravity();

        b_Controller.Move(b_Movement);
    }

    private void RotationByPlane()
    {
        if (Mathf.Abs(b_Controller.velocity.x) >= 1f)
        {
            if (Physics.Raycast(b_rayCastRight.position, -transform.up, out b_hitFirstPoint))
            {
                //normalHitFirst = hitFirstPoint.normal;
                Debug.DrawRay(b_rayCastRight.position, transform.up);
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitFirstPoint.normal);
            }
            if (Physics.Raycast(b_rayCastLeft.position, -transform.up, out b_hitSecondPoint))
            {
                //normalHitSecond = hitFirstPoint.normal;
                Debug.DrawRay(b_rayCastLeft.position, transform.up);
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitSecondPoint.normal);
            }
        }

        else if (Mathf.Abs(b_Controller.velocity.z) >= 1f)
        {
            if (Physics.Raycast(b_rayCastBack.position, -transform.up, out b_hitFirstPoint))
            {
                //normalHitFirst = hitFirstPoint.normal;
                Debug.DrawRay(b_rayCastBack.position, transform.up);
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitFirstPoint.normal);
            }
            if (Physics.Raycast(b_rayCastFront.position, -transform.up, out b_hitSecondPoint))
            {
                //normalHitSecond = hitFirstPoint.normal;
                Debug.DrawRay(b_rayCastFront.position, transform.up);
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitSecondPoint.normal);
            }
        }

        if (b_hitFirstPoint.normal != b_hitSecondPoint.normal && !b_onSlope)
        {
            b_onSlope = true;
            b_rotating = true;

            if(b_hitFirstPoint.normal != Vector3.left && b_hitFirstPoint.normal != Vector3.right
                && b_hitSecondPoint.normal != Vector3.left && b_hitSecondPoint.normal != Vector3.right)
            {
                if (b_hitFirstPoint.normal != new Vector3(0f, 1f, 0f))
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal);
                    //transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);
                    //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal), b_speed);
                    //b_desiredRotation = b_hitFirstPoint.normal;
                }
                else
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, b_hitSecondPoint.normal);
                    //transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), Time.deltaTime* b_speed);
                    //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), b_speed);
                    //b_desiredRotation = b_hitSecondPoint.normal;

                }
            }
            

        }
        else if (b_hitFirstPoint.normal == b_hitSecondPoint.normal)
        {
            if (b_hitFirstPoint.normal == Vector3.up)
            {
                b_onSlope = false;
                b_rotating = false;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, b_hitFirstPoint.normal);
                //this.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal),b_speed);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);

                //StartCoroutine("FlyRotation");
                //b_desiredRotation = b_hitFirstPoint.normal;
            }
        }
    }

    private void SetGravity()
    {
        if (b_Controller.isGrounded)
        {

            b_Movement.y = -1f * Time.deltaTime;
        }
        else
        {
            b_Movement.y -= 1f * Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 500, 100), "Normal" + b_hitFirstPoint.normal + " Normal2: " + b_hitSecondPoint.normal);
        GUI.Label(new Rect(10, 30, 500, 100), "Normal del cubo" + -transform.up);

        GUI.Label(new Rect(60, 100, 500, 100), "Velocity X: " + Mathf.Abs(b_Controller.velocity.x));
        GUI.Label(new Rect(60, 120, 500, 100), "Velocity Z: " + Mathf.Abs(b_Controller.velocity.z));
        GUI.Label(new Rect(60, 140, 500, 100), "On slope: " + b_onSlope);



    }

}

   
