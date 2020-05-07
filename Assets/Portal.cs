using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    private Transform pass;
    private GameObject box;
    private bool passingTrough;
    private Vector3 newVelocity;
    private bool teleported;
    public GameObject myPrefab;
    private GameObject boxcopy;
    public float distance;
    public float dis_to_reset_col;
    public int layerBoxes;
    public int layerPassable;
    public int layerFloor;
    Vector3 originalBoxtransformPos;
    Quaternion originalBoxrotation;
    Vector3 auxDir;

    private GameObject directionObject;

    public bool isPortal_1;

    //This is A mierda, PLEASE check it
    void Start()
    {
        directionObject = GetComponentInChildren<Transform>().gameObject;
        dis_to_reset_col = 0.2f;
        passingTrough = false;
        teleported = false;
        if (!isPortal_1)
            destination = GameObject.FindGameObjectWithTag("Portal_1").GetComponent<Transform>();
        else
            destination = GameObject.FindGameObjectWithTag("Portal_2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (passingTrough)
        {  
            if (this.transform.position.y + .2f > box.transform.position.y  && !teleported)
            {
                boxcopy = Instantiate(myPrefab, new Vector3(destination.position.x - .6f, destination.position.y-.8f, destination.position.z), originalBoxrotation);

                boxcopy.layer = layerPassable;
                boxcopy.GetComponent<BoxController>().b_moveDirection = new Vector3(-auxDir.y + 2f, 0f, 0f);
                teleported = true;
                Debug.Log("Entra");
            }
            if (teleported)
            {
                if(boxcopy.transform.position.x > destination.position.x + dis_to_reset_col)
                {
                    Destroy(box);
                    passingTrough = false;
                    teleported = false;
                    boxcopy.layer = layerBoxes;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            box = other.gameObject;
            box.layer = layerPassable;
            //Physics.IgnoreLayerCollision(layerFloor, layerPassable);
            //Physics.IgnoreLayerCollision(layerBoxes, layerPassable);

            passingTrough = true;

            originalBoxrotation = box.transform.rotation;
            auxDir = box.GetComponent<BoxController>().b_moveDirection;


        }

    }
    //private void OnGUI()
    //{
    //    GUIStyle guiStyle = new GUIStyle(); //create a new variable
    //    guiStyle.fontSize = 30;
    //    if(box != null)GUI.Label(new Rect(10, 100, 500, 100), "Box: " + box.transform.position.y , guiStyle);
    //    GUI.Label(new Rect(10, 200, 500, 100), "Portal: " + this.transform.position.y, guiStyle);


    //}
}                                                                                              