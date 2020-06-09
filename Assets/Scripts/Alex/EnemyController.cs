using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    private Transform Player;
    public NavMeshAgent nav;
    private Transform startTransform;
    private float multiplyBy = 3f;
    private Rigidbody rb;
    private Animator e_anim;
    public bool Grounded;
    public bool itsounds;
    int PlayerFaceState;
    // Start is called before the first frame update
    void Start()
    {
        itsounds = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        e_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grounded) { Move(); rb.mass = 600; }

        e_anim.SetFloat("Speed", nav.velocity.magnitude);
        if (nav.velocity.magnitude > 0.1 && !itsounds)
        {
            if (AudioController.AudioInstance) AudioController.AudioInstance.soundEnemySteps(true); itsounds = true;
        }
        else if (nav.velocity.magnitude < 0.1 && itsounds)
        {
            if (AudioController.AudioInstance) AudioController.AudioInstance.soundEnemySteps(false); itsounds = false;
        }
    }

    void Move()
    {
        PlayerFaceState = Player.gameObject.GetComponent<PlayerControl>().FaceState;
        e_anim.SetInteger("Behaviour", PlayerFaceState);
        if(PlayerFaceState == 0)
        {
            nav.enabled = false;
            rb.velocity = new Vector3(0, 0, 0);
            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (PlayerFaceState == 1)
        {
            //rb.constraints = RigidbodyConstraints.None;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            nav.enabled = true;
            if (Vector3.Distance(transform.position, Player.position) > 2) { nav.SetDestination(Player.position); }
            else
            {
                nav.enabled = false;
            }
        }
        else
        {
            //rb.constraints = RigidbodyConstraints.None;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;
            nav.enabled = true;
            RunFrom();
        }
    }


    void RunFrom()
    {

        // store the starting transform
        startTransform = transform;

        //temporarily point the object to look away from the player
        //transform.rotation = Quaternion.LookRotation(transform.position - Player.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + (transform.position - Player.position) * multiplyBy;
        runTo = new Vector3(runTo.x,transform.position.y,runTo.z);
        //Debug.Log("runTo = " + runTo);

        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

        NavMeshHit hit;    // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
        //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        // just used for testing - safe to ignore
       // nextTurnTime = Time.time + 5;

        // reset the transform back to our start transform
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        // And get it to head towards the found NavMesh position
        nav.SetDestination(hit.position);
    }

    private void OnTriggerStay(Collider other)
    {
        Grounded = gameObject.layer != 11 ? /*other.gameObject.layer == 9 || other.gameObject.layer == 17 || other.gameObject.layer == 8 || other.gameObject.layer == 14 || other.gameObject.layer == 15 ?*/ true : false /* : false*/;
    }
}

