using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour
{
    public Transform transformC;
    public GameObject player;

    private void Awake()
    {
        this.transform.position = transformC.position;
        this.transform.rotation = transformC.rotation;
    }
    private void Update()
    {
        this.transform.position = new Vector3(transformC.position.x,transformC.position.y + 1.5f, transformC.position.z) + transformC.forward * 2f;
        this.transform.rotation = transformC.rotation;

    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("walls") || other.CompareTag("forceFields") || (other.CompareTag("Box") && player.GetComponent<PlayerControl>().box != null && !player.GetComponent<PlayerControl>().box.Equals(other.gameObject) && other.GetType() != typeof(SphereCollider)))
            transformC.gameObject.GetComponent<PlayerControl>().collisionWhileGrabbing = true;


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor") || other.CompareTag("walls") || other.CompareTag("forceFields") || (other.CompareTag("Box") && player.GetComponent<PlayerControl>().box != null && !player.GetComponent<PlayerControl>().box.Equals(other.gameObject) && other.GetType() != typeof(SphereCollider)))
            transformC.gameObject.GetComponent<PlayerControl>().collisionWhileGrabbing = false;
    }

}
