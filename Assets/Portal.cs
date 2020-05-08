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
    private GameObject boxcopy;
    private Vector3 originalBoxtransformPos;
    private Quaternion originalBoxrotation;
    private Vector3 auxDir;

    public GameObject myPrefab;
    public float distance;
    public float dis_to_reset_col;
    public int layerBoxes;
    public int layerPassable;
    public int layerFloor;

    //Raycast
    [SerializeField] private GameObject directionObject;
    [SerializeField] private LayerMask LayersToCast;
    [HideInInspector] public string portal_face = "";
    private RaycastHit ray;


    public bool isPortal_1;

    //This is A mierda, PLEASE check it
    void Start()
    {
        calculatePortalPosition();
        dis_to_reset_col = 0.2f;
        passingTrough = false;
        teleported = false;
        if (!isPortal_1)
            destination = GameObject.FindGameObjectWithTag("Portal_1").GetComponent<Transform>();
        else
            destination = GameObject.FindGameObjectWithTag("Portal_2").GetComponent<Transform>();
    }

    private void calculatePortalPosition()
    {
        //Esto ya funciona (solo he puesto esas posiciones porque son las unicas en las que se ve el portal en isometrica)
        Physics.Raycast(directionObject.transform.position, -directionObject.transform.forward, out ray, 10.0f, LayersToCast);
        Debug.DrawLine(directionObject.transform.position, ray.point);
        if (ray.normal.y == 1f)
        {
            portal_face = "y+";
        }
        else if (ray.normal.z == 1f)
        {
            portal_face = "z+";
        }
        else if (ray.normal.x == 1f)
        {
            portal_face = "x+";
        }
        /*
        else if (ray.normal.Equals(Vector3.right))
        {
            portal_face = "right(x-)";
        }
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        print("Portal is facing: " + portal_face);
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
   /* private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        //if(box != null)GUI.Label(new Rect(10, 100, 500, 100), "Box: " + box.transform.position.y , guiStyle);
        // GUI.Label(new Rect(10, 200, 500, 100), "Portal: " + this.transform.position.y, guiStyle);
        GUI.Label(new Rect(10, 200, 500, 100), "Portal: " + ray.normal, guiStyle);

    }
    */
}                                                                                              