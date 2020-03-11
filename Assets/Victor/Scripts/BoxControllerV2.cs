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
    [Range(0.0f, 100.0f)] public float b_speed;


    RaycastHit b_hitFirstPoint;
    RaycastHit b_hitSecondPoint;

    Vector3 b_desiredRotation;

    [SerializeField] bool b_onSlope;
    private bool b_rotationInProgress;
    float lerpTime;

    // Start is called before the first frame update

    private void Awake()
    {
        b_Controller = this.GetComponent<CharacterController>();
    }
    void Start()
    {
        b_onSlope = false;
        b_rotationInProgress = false;
        lerpTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    
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
        RotationByPlane();


        b_Controller.Move(b_Movement);
    }

    private void RotationByPlane()
    {
        if (Mathf.Abs(b_Controller.velocity.x) >= 1f)
        {
            if (Physics.Raycast(b_rayCastRight.position, -transform.up, out b_hitFirstPoint))
            {
                Debug.DrawRay(b_rayCastRight.position, transform.up);
                //normalHitFirst = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitFirstPoint.normal);
            }
            if (Physics.Raycast(b_rayCastLeft.position, -transform.up, out b_hitSecondPoint))
            {
                Debug.DrawRay(b_rayCastLeft.position, transform.up);
                //normalHitSecond = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitSecondPoint.normal);
            }
        }

        else if (Mathf.Abs(b_Controller.velocity.z) >= 1f)
        {
            if (Physics.Raycast(b_rayCastBack.position, -transform.up, out b_hitFirstPoint)) //Return normal of plane
            {
                Debug.DrawRay(b_rayCastBack.position, transform.up);
                //normalHitFirst = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitFirstPoint.normal);
            }
            if (Physics.Raycast(b_rayCastFront.position, -transform.up, out b_hitSecondPoint)) //Return normal of plane
            {
                Debug.DrawRay(b_rayCastFront.position, transform.up);
                //normalHitSecond = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitSecondPoint.normal);
            }
        }


        if (b_hitFirstPoint.normal != b_hitSecondPoint.normal && !b_onSlope) //Both plane normal != 
        {
            print("Normal Changed");
            b_onSlope = true;
            b_rotationInProgress = true;
            lerpTime = 0f;

            //if (b_hitFirstPoint.normal != Vector3.up)
            //{
            //    transform.rotation = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal,);
            //    //transform.rotation = Quaternion.Lerp(Quaternion.Euler(this.transform.up), Quaternion.Euler(b_hitFirstPoint.normal), 0.2f);
            //    //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal), b_speed);
            //    //b_desiredRotation = b_hitFirstPoint.normal;
            //}
            //else
            //{
            //    transform.rotation = Quaternion.FromToRotation(transform.up, b_hitSecondPoint.normal);
            //    //transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), Time.deltaTime* b_speed);
            //    //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), b_speed);
            //    //b_desiredRotation = b_hitSecondPoint.normal;
            //}
            if (b_hitFirstPoint.normal != Vector3.up)
            {
                Debug.Log("Entra 1");
                //transform.rotation = Quaternion.FromToRotation(this.transform.rotation,Quaternion.Euler(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, Time.deltaTime));
                //transform.rotation.SetEulerAngles(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, b_speed * Time.deltaTime));
                //transform.rotation = Quaternion.Slerp(Quaternion.Euler(this.transform.up), Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal),
                    Time.deltaTime * Mathf.Abs(b_Controller.velocity.sqrMagnitude) * b_speed);
                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal);
                //transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), lerpTime);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);
                //b_desiredRotation = b_hitFirstPoint.normal;
            }
            else
            {
                Debug.Log("Entra 2");

                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.up, b_hitSecondPoint.normal, Time.deltaTime));

                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitSecondPoint.normal);
                //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), b_speed);
                //b_desiredRotation = b_hitSecondPoint.normal;

            }

        }

        else if (b_hitFirstPoint.normal == b_hitSecondPoint.normal)
        {
            if (b_hitFirstPoint.normal == Vector3.up)
            {
                b_onSlope = false;
                transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.FromToRotation(Vector3.up, b_hitFirstPoint.normal),
                    Time.deltaTime * Mathf.Abs(b_Controller.velocity.sqrMagnitude)* .1f);
                //this.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal),b_speed);
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);

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
        GUI.Label(new Rect(10, 10, 500, 100), "Normal" + b_hitFirstPoint.normal + " Normal2: " + b_hitSecondPoint.normal);
        GUI.Label(new Rect(10, 40, 500, 100), "Normal del cubo" + -transform.up);
        GUI.Label(new Rect(10, 70, 500, 100), "Rotating " + b_rotationInProgress);
        GUI.Label(new Rect(10, 100, 500, 100), "OnSlope " + b_onSlope);


        GUI.Label(new Rect(10, 130, 500, 100), "Velocity X: " + Mathf.Abs(b_Controller.velocity.x));
        GUI.Label(new Rect(10, 160, 500, 100), "Velocity Z: " + Mathf.Abs(b_Controller.velocity.z));



    }

}

   
