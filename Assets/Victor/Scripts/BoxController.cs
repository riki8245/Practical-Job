using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    Rigidbody b_RigidBody;
    [SerializeField]bool b_Movable;
    GameObject b_activePlayer;

    [SerializeField]float b_timeHold;
    private Vector3 b_Direction;
    private float b_SpeedTimeCounter = 0f;
    bool b_PushedOrPulled;
    Vector3 normalhit;


    private void Awake()
    {
        b_Movable = false;
        b_RigidBody = this.GetComponent<Rigidbody>();
        b_timeHold = 0f;
        b_Direction = new Vector3();
    }
    // Start is called before the first frame update
    void Start()
    {
        b_RigidBody.constraints = RigidbodyConstraints.FreezeRotationY;
        b_RigidBody.constraints = RigidbodyConstraints.FreezeRotationX;
        b_RigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;

    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion quaternion = this.transform.rotation;
        //quaternion.y = 0f;
        //this.transform.rotation = quaternion;
        //this.transform.rotation.SetFromToRotation(new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z), new Vector3(this.transform.rotation.x, 0f, this.transform.rotation.z));
        
    }

    public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
    {
        return vector - Vector3.Project(vector, planeNormal);
    }

    public static Vector3 Project(Vector3 vector, Vector3 onNormal)
    {
        float num = Vector3.Dot(onNormal, onNormal);
        if (num < Mathf.Epsilon)
        {
            return Vector3.zero;
        }
        return onNormal * Vector3.Dot(vector, onNormal) / num;
    }

    private void FixedUpdate()
    {
        //b_RigidBody.MovePosition(transform.position + Vector3.left * Time.deltaTime);
        //b_RigidBody.velocity = Vector3.left * Time.deltaTime * 100;
        transform.Translate(transform.right * Time.deltaTime);
        //Vector3.ProjectOnPlane()

        //RaycastHit hit;


        //if (Physics.Raycast(transform.position, -transform.up, out hit))
        //{
        //    normalhit = hit.normal;
        //    Debug.DrawRay(transform.position, -transform.up);
        //}

        //transform.Translate(ProjectOnPlane(-transform.right, normalhit) * Time.deltaTime);


        //float lerpVelocity = 1f;
        //Vector3.Lerp(this.transform.up, normalhit, Time.deltaTime * lerpVelocity);


        //b_RigidBody.AddForce(Vector3.left,ForceMode.VelocityChange);
        //transform.position += Vector3.left * Time.deltaTime;

        //if (b_activePlayer != null)
        //{
        //    PlayerController activePlayer = b_activePlayer.GetComponent<PlayerController>();
        //    b_Direction = activePlayer.b_ForceDirection;

        //    if (b_Movable && activePlayer.p_CanGrabObject && Input.GetButton("Fire1"))
        //    {
        //        activePlayer.p_PushingOrPulling = true;
        //        b_timeHold += 0.02f;
        //        b_timeHold = Mathf.Clamp(b_timeHold, 0f, 3f);

        //        //b_RigidBody.MovePosition(transform.position + b_Direction * Time.deltaTime * (b_timeHold+1));
        //        //b_RigidBody.AddForce(Vector3.forward);
        //        //print(activePlayer.b_ForceDirection);
        //        //ApplyForceToReachVelocity(b_RigidBody, Vector3.forward);
        //        transform.position += b_Direction * Time.deltaTime * (b_timeHold+1f);
        //        //transform.Translate(activePlayer.b_ForceDirection * Time.deltaTime * b_timeHold);
        //        //b_RigidBody.AddForceAtPosition(activePlayer.b_ForceDirection * 1000 * Time.deltaTime * (b_timeHold + 1), this.GetComponentInChildren<Transform>().position);
        //        //b_RigidBody.velocity = activePlayer.b_ForceDirection * 10 * Time.deltaTime * (b_timeHold + 1);
        //        print(b_RigidBody.velocity);
        //        print(activePlayer.b_ForceDirection);
        //        b_SpeedTimeCounter = 3f;
        //    }
        //    else
        //    {
        //        activePlayer.p_PushingOrPulling = false;

        //        while (b_SpeedTimeCounter >= 0f)
        //        {
        //            print("Deja de pulsar" + b_RigidBody.velocity);

        //            b_SpeedTimeCounter -= Time.deltaTime;
        //            b_RigidBody.velocity = b_Direction * 10 * Time.deltaTime * b_SpeedTimeCounter;
        //            b_PushedOrPulled = false;


        //        }
        //    }


        //}






        //else
        //{
        //    Invoke("SlowDownObject",0.1f);
        //    activePlayer.p_PushingOrPulling = false;
        //    //b_RigidBody.velocity = 0f;
        //}
    }

    /*private void SlowDownObject ()
    {
        print("entra en slowDown");
        while(b_timeHold >= 0f)
        {
            b_timeHold -= Time.deltaTime;
            b_timeHold = Mathf.Floor(b_timeHold);
            print(b_timeHold);
            b_RigidBody.velocity = b_Direction * 1000 * Time.deltaTime * b_timeHold;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            b_activePlayer = other.gameObject;
            b_Movable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<PlayerController>().p_PushingOrPulling) {
                b_Movable = false;
            }

        }
    }*/

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 100), "Euler" + transform.rotation.eulerAngles);
        GUI.Label(new Rect(10, 30, 500, 100), "Normal" + transform.rotation);
    }

    
}
