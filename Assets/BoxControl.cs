using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    private bool canMove;

    // Start is called before the first frame update
    void Awake()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            this.transform.position -= new Vector3(0f, 9.8f * Time.deltaTime *  Time.deltaTime * 10f, 0f);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
            canMove = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
            canMove = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = null;
        }
    }

    public void Move()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().velocity = Vector3.forward;
        StartCoroutine(MoveBox());
    }

    private IEnumerator MoveBox()
    {
        yield return new WaitForSeconds(5f);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}
