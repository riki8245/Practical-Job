using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform target;
    public float speed;
    public GameObject player;

    [HideInInspector] public Vector3 start, end;
    private bool touching;

    // Start is called before the first frame update
    void Start()
    {
        if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        if(transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }




}
