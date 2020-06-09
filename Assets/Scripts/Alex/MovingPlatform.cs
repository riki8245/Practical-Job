using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform target;
    public float speed;
    public List<GameObject> ThingsInPlatform;

    [HideInInspector] public Vector3 start, end;
    private bool touching;
    private bool change = false;
    private float timer = 0;

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

  
    private void FixedUpdate()
    {
        if (change)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position, 0);
            if (timer < 1) { timer += Time.deltaTime; }
            else { change = false; timer = 0; }
        }
        else
        {

            if (target != null)
            {
                float fixedSpeed = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
            }

            if (target.position == transform.position)
            {
                change = true;
                target.position = (target.position == start) ? end : start;
            }
        }
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ThingsInPlatform.Contains(other.gameObject))
        {
            other.gameObject.transform.parent = transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Box"))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
