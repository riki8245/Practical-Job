using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForceField : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Forbbidden;
    private bool showing;
    void Start()
    {
        showing = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        print("exit");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!showing) StartCoroutine("showSign");

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("in");
        /*
        ContactPoint contact = collision.contacts[0];
        
        Forbbidden.transform.position = contact.point;
        Forbbidden.transform.rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);*/
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!showing) StartCoroutine("showSign");

        }
    }

    private IEnumerator showSign()
    {
        showing = true;
        Forbbidden.GetComponent<MeshRenderer>().enabled = true;

        yield return new WaitForSeconds(1.5f);

        showing = false;
        Forbbidden.GetComponent<MeshRenderer>().enabled = false;
    }
}
