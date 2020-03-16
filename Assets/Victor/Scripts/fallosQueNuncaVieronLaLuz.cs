using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallosQueNuncaVieronLaLuz : MonoBehaviour
{
    /*void Update()
    {
        //RotationByPlane();
        
        if (b_moveBox)
        {
            b_Movement = new Vector3(b_targetMovement.x, 0f, b_targetMovement.z);
            b_lastKnownMovement = b_Movement;

        }
        else
        {
            //b_Movement = (!b_Movement.Equals(Vector3.zero)) ? b_Movement - (Time.fixedDeltaTime * b_Movement) : Vector3.zero;
            b_Movement = Vector3.Lerp(b_Movement, new Vector3(0f, b_Movement.y, 0f), Time.deltaTime * 2f);
        }
            //SetGravity();
        
        //else
        //    b_Movement = Vector3.zero; 

        b_Controller.Move(b_Movement * Time.deltaTime);
    }
    #region
    //if (b_playerIsOut && b_Controller.velocity == Vector3.zero)
    //{
    //    b_grabableObject = true;
    //}

    //if (!b_grabableObject) //Two main reasons: velocity > 0, already grabbed
    //{

    //if (b_isGettingMoved)
    //{
    //    if(p_Movement == Vector3.zero && b_Movement != Vector3.zero) //Player gets stopped but box has velocity
    //    {
    //        b_isGettingMoved = false;
    //    }
    //    else
    //    {
    //        b_Movement.x = p_Movement.x;
    //        b_Movement.z = p_Movement.z;

    //    }
    //    b_velocity = b_Controller.velocity.magnitude;

    //}
    //if(!b_isGettingMoved)
    //{
    //    b_Movement.x =  b_Movement.x * b_velocity;
    //    b_Movement.z = b_Movement.z * b_velocity;
    //    b_velocity -= Time.fixedDeltaTime * 10;
    //    Mathf.Clamp(b_velocity, 0f,5f);
    //}
    //}

    //if (b_Controller.velocity == Vector3.zero)
    //{
    //    b_FirstInputOfPlayer = false; //cant move box anymore until velocity = 0
    //    b_Movement = Vector3.zero;
    //    b_grabableObject = true;
    //    return;
    //}

    //}
    #endregion

    private void SetGravity()
    {
        if (b_Controller.isGrounded)
        {
            isGrounded = true;
            b_fallVelocity = -b_gravity * Time.deltaTime;
            b_Movement.y = b_fallVelocity;
        }

        else
        {
            isGrounded = false;
            b_fallVelocity -= b_gravity * Time.deltaTime;
            b_Movement.y = b_fallVelocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entra");
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (Input.GetButtonDown("Fire1") && b_grabableObject)
            {
                b_grabableObject = false;
                b_FirstInputOfPlayer = true; //1st time starts to pull or push
                //playerController.slightProceduralAnimation();
            }

            if (playerController.p_CanMoveBox && Input.GetButton("Fire1") && b_FirstInputOfPlayer) //Movable box + first time pressing button
            {
                playerController.p_PushingOrPulling = true;
                b_targetMovement = playerController.p_moveDirection;
                b_targetMovement.y = 0f;
                if (!b_targetMovement.Equals(Vector3.zero))
                    b_moveBox = true;
                else
                    b_moveBox = false;

            }

            if(Input.GetButtonUp("Fire1"))
            {
                b_moveBox = false;
                playerController.p_PushingOrPulling = false;
                b_Movement = b_lastKnownMovement;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.p_PushingOrPulling = false;
            b_moveBox = false;
            b_FirstInputOfPlayer = false;
            b_grabableObject = true;
        }
    }
    private void RotationByPlane()
    {

        /*if (b_rotating)   ESTO ESTABA EN EL UPDATE
        {
            timer += Time.deltaTime;
            //this.transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(b_desiredRotation),timer);
            this.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitFirstPoint.normal), 1f*Time.deltaTime);

        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.up), Quaternion.Euler(b_desiredRotation), Time.deltaTime * b_speed);

        }
        if (Mathf.Abs(b_Controller.velocity.x) >= 0.1f)
        {
            if (Physics.Raycast(b_rayCastRight.position, -transform.up, out b_hitFirstPoint))
            {
                //Debug.DrawRay(b_rayCastRight.position, transform.up);
                Debug.DrawRay(b_rayCastRight.position, b_hitFirstPoint.normal);
                //normalHitFirst = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitFirstPoint.normal);
            }
            if (Physics.Raycast(b_rayCastLeft.position, -transform.up, out b_hitSecondPoint))
            {
                //Debug.DrawRay(b_rayCastLeft.position, transform.up);
                Debug.DrawRay(b_rayCastLeft.position, b_hitFirstPoint.normal);

                //normalHitSecond = hitFirstPoint.normal;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, hitSecondPoint.normal);
            }
        }
        
        else if (Mathf.Abs(b_Controller.velocity.z) >= 0.1f)
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
        
       
        if (b_hitFirstPoint.normal != b_hitSecondPoint.normal) //Both plane normal != 
        {
            print("Normal Changed");
            b_onSlope = true;
            b_rotationInProgress = true;
            lerpTime += Time.deltaTime;

            if (b_hitFirstPoint.normal != Vector3.up)
            {
                actual = this.transform.rotation;
                target = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal);
                //Vector3 newDir = Vector3.RotateTowards(transform.up, b_hitFirstPoint.normal,b_speed*Time.deltaTime, 0.0f);
               // transform.rotation = Quaternion.Lerp(this.transform.rotation,Quaternion.Euler(newDir),lerpTime);
                //transform.rotation = Quaternion.FromToRotation(this.transform.rotation,Quaternion.Euler(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, Time.deltaTime));
                //transform.rotation.SetEulerAngles(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, b_speed * Time.deltaTime));
                //transform.rotation = Quaternion.Slerp(Quaternion.Euler(this.transform.up), Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime);
                //transform.rotation = Quaternion.Slerp(actual,target,Time.deltaTime * lerpTime);
                //transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal),
                //    Time.deltaTime * Mathf.Abs(b_Controller.velocity.sqrMagnitude) * b_speed);
                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal);
                //transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), lerpTime);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);
                //b_desiredRotation = b_hitFirstPoint.normal;
            }
            else if (b_hitSecondPoint.normal != Vector3.up)
            {
                

                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.up, b_hitSecondPoint.normal, Time.deltaTime));

                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitSecondPoint.normal);
                //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), b_speed);
                //b_desiredRotation = b_hitSecondPoint.normal;

            }

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


        }
        else
        {
            lerpTime = 0f;
        }
        /*
        else if (b_rotationInProgress)
        {
            Debug.Log("Entra");
            if (b_hitFirstPoint.normal != Vector3.up)
            {
                Debug.Log(b_hitFirstPoint.normal);
                Debug.Log("Entra 1");
                //transform.rotation = Quaternion.FromToRotation(this.transform.rotation,Quaternion.Euler(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, Time.deltaTime));
                //transform.rotation.SetEulerAngles(Vector3.Lerp(transform.up, b_hitFirstPoint.normal, b_speed * Time.deltaTime));
                //transform.rotation = Quaternion.Slerp(Quaternion.Euler(this.transform.up), Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal),
                    Time.deltaTime * Mathf.Abs(b_Controller.velocity.sqrMagnitude) * b_speed);
                //transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal),
                //    Time.deltaTime * Mathf.Abs(b_Controller.velocity.sqrMagnitude) * b_speed);
                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitFirstPoint.normal);
                //transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), lerpTime);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(b_hitFirstPoint.normal), Time.deltaTime * b_speed);
                //b_desiredRotation = b_hitFirstPoint.normal;
            }
            else if(b_hitSecondPoint.normal != Vector3.up)
            {
                Debug.Log("Entra 2");

                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.up, b_hitSecondPoint.normal, Time.deltaTime));

                //transform.rotation = Quaternion.FromToRotation(transform.up, b_hitSecondPoint.normal);
                //transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.up), Quaternion.Euler(b_hitSecondPoint.normal), b_speed);
                //b_desiredRotation = b_hitSecondPoint.normal;

            }

            if(this.transform.up == b_hitSecondPoint.normal && this.transform.up == b_hitFirstPoint.normal)
            {
                b_rotationInProgress = false;
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
        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControllerRigibody : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody b_Controller;

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

    // Start is called before the first frame update

    private void Awake()
    {
        b_Controller = this.GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        b_horizontalMove = Input.GetAxis("Horizontal");
        b_verticalMove = Input.GetAxis("Vertical");
        b_isGrounded = Physics.CheckSphere(this.transform.position - new Vector3(0f, 0.5f, 0f), .2f, layerMaskFloor);
        CheckSlope();
        MoveBox();
        //SetGravity();
        b_Controller.velocity = b_moveDirection;
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


        if (b_isGrounded)
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
            if (Physics.Raycast(b_rayCastRight.position, -transform.up * 10, out b_hitFirstPoint) &&
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.p_PushingOrPulling = false;
            playerController = null;
            b_isGettingMoved = false;
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

            if (Input.GetButtonUp("Fire1"))
            {
                playerController.p_PushingOrPulling = false;
                p_AlreadyPressed = false;
                playerController.axisToUseWhileBox = 0;
            }

            if (b_isGettingMoved)
            {
                if (playerController.axisToUseWhileBox == 1)
                    b_moveDirection = Vector3.right * b_horizontalMove * b_Speed;
                else if (playerController.axisToUseWhileBox == 2)
                    b_moveDirection = Vector3.forward * b_verticalMove * b_Speed;
            }
        }
        else
            b_moveDirection = Vector3.zero;




        //b_moveDirection = p_stopsPressing && b_moveDirection != Vector3.zero? Vector3.Lerp(b_moveDirection, Vector3.zero, 1) : b_moveDirection;





    }
}

    }*/
}
