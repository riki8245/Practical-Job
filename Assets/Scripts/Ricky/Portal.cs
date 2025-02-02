﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private Transform destination;
    private GameObject objectTeleporting, boxcopy;
    private Quaternion originalBoxrotation;
    private RaycastHit ray;
    private Vector3 auxDir, InstantiateBoxPos, outvec;
    private float portalFacingAxis, boxEnteringAxis, boxExitAxis, DestinationPos;
    private bool passingTrough, teleported;
    private int layerBoxes, layerPassable;

    //Parameters needed;
    public GameObject BoxPrefab, EnemyPrefab, directionObject, particlesInEnter;
    public LayerMask LayersToCast;

    [HideInInspector] public string portal_face = "";


    //Modify Exit of the destination portal
    public float dis_to_reset_col, ForceAddedForExit, PercentForce;
    [Header("Only if the Exit portal is facing Y+ (Between 0-1)")]
    public float AmountForceOnY;
    public float AmountForceOnZ;
    public float AmountForceOnX;
    public bool ControlForceManually;

    public bool isPortal_1;

    private void Awake()
    {
        layerBoxes = 8;
        layerPassable = 11;
        if (!isPortal_1)
            destination = GameObject.FindGameObjectWithTag("Portal_1").GetComponent<Transform>();
        else
            destination = GameObject.FindGameObjectWithTag("Portal_2").GetComponent<Transform>();
    }

    void Start()                                  
    {
        calculatePortalPosition();
        dis_to_reset_col = 2.35f;
        passingTrough = false;
        teleported = false;
        destination.gameObject.GetComponent<Portal>().calculateDestinationBoxPos();
    }

    private void calculateDestinationBoxPos()
    {
        switch (destination.GetComponent<Portal>().portal_face)
        {
            case "y+": InstantiateBoxPos = new Vector3(destination.position.x, destination.position.y - .6f, destination.position.z);
                DestinationPos = destination.position.y;
                outvec = new Vector3(AmountForceOnX, AmountForceOnY, AmountForceOnZ);
                break;
            case "z+": InstantiateBoxPos = new Vector3(destination.position.x, destination.position.y - .8f, destination.position.z - .6f);
                DestinationPos = destination.position.z;
                outvec = Vector3.forward;
                break;
            case "x+": InstantiateBoxPos = new Vector3(destination.position.x - .6f, destination.position.y - .8f, destination.position.z);
                DestinationPos = destination.position.x;
                outvec = Vector3.right;
                break;
            default: break;
        }
    }

    private void calculatePortalPosition()
    {
        //Esto ya funciona (solo he puesto esas posiciones porque son las unicas en las que se ve el portal en isometrica)
        Physics.Raycast(directionObject.transform.position, -directionObject.transform.forward, out ray, 2.5f, LayersToCast);
        //Debug.DrawLine(directionObject.transform.position, ray.point);
        if (ray.normal.y == 1f)
        {
            portal_face = "y+";
            portalFacingAxis = this.transform.position.y;
        }
        else if (Mathf.Abs(ray.normal.z) == 1f)
        {
            portal_face = "z+";
            portalFacingAxis = this.transform.position.z;
        }
        else if (Mathf.Abs(ray.normal.x) == 1f)
        {
            portal_face = "x+";
            portalFacingAxis = this.transform.position.x;
        }
       /* else if (ray.normal.y == -1f)  //the rest of the axis cant be shown show i dont aply the raycast
        {
            portal_face = "y-";
        }
        else if (ray.normal.z == -1f)  //the rest of the axis cant be shown show i dont aply the raycast
        {
            portal_face = "z-";
        } 
        else if (ray.normal.x == -1f)  //the rest of the axis cant be shown show i dont aply the raycast
        {
            portal_face = "x-";
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPortal_1) print("Portal 1 is facing: " + portal_face + ", My transform: " + transform.position + ", Destination: " + destination.position);
        //else print("Portal 2 is facing: " + portal_face + ", My transform: " + transform.position + ", Destination: " + destination.position);

        if (!portal_face.Equals("") && passingTrough)
        {
            switch (portal_face)
            {
                case "y+": boxEnteringAxis = objectTeleporting.transform.position.y; break;
                case "z+": boxEnteringAxis = objectTeleporting.transform.position.z; break;
                case "x+": boxEnteringAxis = objectTeleporting.transform.position.x; break;
                default: break;
            }

            if (portalFacingAxis + .2f > boxEnteringAxis && !teleported)
            {
                calculateDestinationBoxPos();
                boxcopy = objectTeleporting.CompareTag("Box")? Instantiate(BoxPrefab, InstantiateBoxPos, originalBoxrotation) : Instantiate(EnemyPrefab, InstantiateBoxPos, originalBoxrotation);
                Vector3 scales = boxcopy.transform.localScale;
                boxcopy.transform.localScale = Vector3.zero;
                iTween.ScaleTo(boxcopy, iTween.Hash("x", scales.x, "y", scales.y, "z", scales.z, "time", .25f));
                if (objectTeleporting.CompareTag("Enemy"))
                {
                    boxcopy.GetComponent<EnemyController>().Grounded = false;
                    boxcopy.GetComponent<EnemyController>().nav.enabled = false;
                    boxcopy.GetComponent<Rigidbody>().mass = 1;
                    //boxcopy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                    //boxcopy.GetComponent<Rigidbody>().isKinematic = false;
                }
                if (objectTeleporting.CompareTag("Box"))
                {
                    boxcopy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    boxcopy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
                //boxcopy.transform.parent = null;
                boxcopy.layer = layerPassable;
                boxcopy.GetComponent<Rigidbody>().AddForce(auxDir * 100f); 
                teleported = true;
            }
            if (teleported)
            {
                switch (destination.GetComponent<Portal>().portal_face)
                {
                    case "y+": boxExitAxis = boxcopy.transform.position.y; break;
                    case "z+": boxExitAxis = boxcopy.transform.position.z; break;
                    case "x+": boxExitAxis = boxcopy.transform.position.x; break;
                    default: break;
                }

                if (boxExitAxis > DestinationPos + dis_to_reset_col /*|| boxExitAxis < DestinationPos - dis_to_reset_col*/)
                {
                    Reset();
                    //boxcopy = null;
                }
            }
        }
    }

    private void Reset()
    {
        if (objectTeleporting.CompareTag("Enemy"))
        {
            //boxcopy.GetComponent<EnemyController>().nav.enabled = true;
        }

        passingTrough = false;
        teleported = false;
        boxcopy.layer = objectTeleporting.CompareTag("Box") ? layerBoxes : 19;
        Destroy(objectTeleporting);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Enemy"))
        {
            try
            {
                if (!destination.GetComponent<Portal>().passingTrough && !destination.GetComponent<Portal>().teleported && !passingTrough && !teleported)
                {
                    if (AudioController.AudioInstance) AudioController.AudioInstance.soundTrespassPortal(true);
                    particlesInEnter.SetActive(true);
                    Invoke("StopParticles", 0.62f);
                    objectTeleporting = other.gameObject;
                    if (other.gameObject.CompareTag("Box"))
                    {
                        objectTeleporting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        objectTeleporting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    }
                    objectTeleporting.layer = layerPassable;
                    if (objectTeleporting.CompareTag("Enemy"))
                    {
                        objectTeleporting.GetComponent<EnemyController>().Grounded = false;
                        //objectTeleporting.GetComponent<Rigidbody>().isKinematic = false;
                        //objectTeleporting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        //objectTeleporting.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        if (objectTeleporting.GetComponent<EnemyController>().nav.enabled == true) objectTeleporting.GetComponent<EnemyController>().nav.enabled = false;
                    }

                    originalBoxrotation = objectTeleporting.transform.rotation;
                    auxDir = objectTeleporting.GetComponent<Rigidbody>().velocity;
                    float FastestAxis = !ControlForceManually ? auxDir.magnitude : 1f;                    

                    switch (portal_face)
                    {
                        case "y+":
                            boxEnteringAxis = objectTeleporting.transform.position.y;
                            auxDir = outvec * (FastestAxis * PercentForce + ForceAddedForExit);
                            break;
                        case "z+":
                            boxEnteringAxis = objectTeleporting.transform.position.z;
                            auxDir = outvec * (FastestAxis * PercentForce + ForceAddedForExit);
                            break;
                        case "x+":
                            boxEnteringAxis = objectTeleporting.transform.position.x;
                            auxDir = outvec * (FastestAxis * PercentForce + ForceAddedForExit);
                            break;
                        default: break;
                    }

                    passingTrough = true;
                    iTween.ScaleTo(other.gameObject, iTween.Hash("x", 0, "y", 0, "z", 0, "time", .6f));
                }
            }
            catch (MissingComponentException e)
            {
                print(e);
            }

        }

    }

    private void StopParticles()
    {
        particlesInEnter.SetActive(false);
    }


    /*
    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        //if(box != null)GUI.Label(new Rect(10, 100, 500, 100), "Box: " + box.transform.position.y , guiStyle);
        // GUI.Label(new Rect(10, 200, 500, 100), "Portal: " + this.transform.position.y, guiStyle);
        GUI.Label(new Rect(10, 200, 500, 100), "Portal: " + ray.normal, guiStyle);

    }
    */

}                                                                                              