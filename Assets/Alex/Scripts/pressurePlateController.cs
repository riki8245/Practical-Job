using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pressurePlateController : MonoBehaviour
{
    public bool pressed = false;
    private Vector3 scaleNotPressed = new Vector3(0.67f, 0.07f, 0.67f);
    private Vector3 scalePressed = new Vector3(0.67f, 0.01f, 0.67f);

    public GameObject door;
    private Animator door_anim;
    [ColorUsage(true, true)]
    public Color red_on, red_off;

    [Header("Change type")]
    [SerializeField] private bool justOneStep;


    // Start is called before the first frame update
    void Start()
    {
        door_anim = door.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            transform.localScale = scalePressed;
        }
        else
        {
            transform.localScale = scaleNotPressed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Enemy"))
        {
            pressed = true;
            transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", red_on);
            door_anim.SetBool("open",true);
            door.GetComponent<CapsuleCollider>().enabled = false;
            /*if (other.gameObject.CompareTag("Box"))
                other.GetComponent<BoxController>().b_moveDirection = Vector3.zero;*/

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!justOneStep)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Enemy"))
            {
                door.GetComponent<CapsuleCollider>().enabled = true;
                transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", red_off);
                pressed = false;
                door_anim.SetBool("open", false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Enemy"))
        {
            door.GetComponent<CapsuleCollider>().enabled = false;
            transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", red_on);
            pressed = true;
            door_anim.SetBool("open", true);
        }
    }

}
