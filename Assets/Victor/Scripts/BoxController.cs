using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] float p_Force;
    GameObject [] p_Players;
    Rigidbody b_RigidBody;
    bool b_Movable;
    string b_activePlayer = "";

    private void Awake()
    {
        b_Movable = false;
        p_Force = 10f;
        p_Players = GameObject.FindGameObjectsWithTag("Player");
        b_RigidBody = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (b_Movable)
        {
            string buttonToGet = "Fire1"; //+ b_activePlayer[-1];
            //int activePlayerAux = b_activePlayer[-1];
            PlayerController activePlayer = p_Players[0].GetComponent<PlayerController>();

            if (Input.GetButton(buttonToGet))
            {
                activePlayer.p_PushingOrPulling = true;
                Vector3 p_Velocity = activePlayer.p_Move;


                b_RigidBody.velocity = p_Velocity * Time.deltaTime;
            }
            else
            {
                activePlayer.p_PushingOrPulling = false;
                b_RigidBody.velocity = Vector3.zero;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            b_activePlayer = other.name;
            b_Movable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            b_Movable = false;
        }
    }
}
