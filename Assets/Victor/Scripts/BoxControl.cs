using System.Collections;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    public string playerSide;// { get; private set; }
    private bool checkPlayerPosition;
    Vector3 direction;
    public bool showOutline;
    public GameObject outline;
    private bool imgettingMoved = false;
    public Transform groundCheck;
    private int whatIsbox = 8;
    const float groundRadius = .1f;

    private void Start()
    {
        StartCoroutine(FreezeConstrainsts());
    }
    public void RotateBox(float slopeAngle, float orientation)
    {
        if (orientation == 90f)
            StartCoroutine(RotateToPosition(Quaternion.Euler(this.transform.localRotation.x, this.transform.localRotation.y, this.transform.localRotation.z - slopeAngle), .2f));
        else if (orientation == -90f)
            StartCoroutine(RotateToPosition(Quaternion.Euler(this.transform.localRotation.x, this.transform.localRotation.y, this.transform.localRotation.z + slopeAngle), .2f));
        else if (orientation == 180f)
            StartCoroutine(RotateToPosition(Quaternion.Euler(this.transform.localRotation.x + slopeAngle, this.transform.localRotation.y, this.transform.localRotation.z), .2f));
        else if (orientation == 0f)
            StartCoroutine(RotateToPosition(Quaternion.Euler(this.transform.localRotation.x - slopeAngle, this.transform.localRotation.y, this.transform.localRotation.z), .2f));
        else
            StartCoroutine(RotateToPosition(Quaternion.Euler(0f, this.transform.localRotation.y, 0f), .2f));
    }
    private void Update()
    {
        if (this.gameObject.layer == 8)
        {
            Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundRadius);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            foreach (Collider item in hitColliders) if (item.gameObject.layer == 9) this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
 
        if (checkPlayerPosition)
        {
            Vector3 rayPoint = this.transform.position + Vector3.up;
            RaycastHit hit;
            if (Physics.Raycast(rayPoint, transform.forward, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inFront";
            else if (Physics.Raycast(rayPoint, -transform.forward, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inBack";
            else if (Physics.Raycast(rayPoint, -transform.right, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inLeft";
            else if (Physics.Raycast(rayPoint, transform.right, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inRight";
            else
                playerSide = "";
        }
        if (imgettingMoved) imgettingMoved = !(this.GetComponent<Rigidbody>().velocity.Equals(Vector3.zero));
        outline.SetActive(showOutline && !imgettingMoved);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = this.gameObject;
            checkPlayerPosition = true;
            playerSide = "";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = null;
            this.transform.parent = null;
            playerSide = "";
            checkPlayerPosition = false;
            if (outline) outline.SetActive(false);
        }
    }

    public void PushBox(float timePressed)
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.GetComponent<Rigidbody>().freezeRotation = true;
        switch (playerSide)
        {
            case "inFront":
                direction = -transform.forward;
                break;
            case "inBack":
                direction = transform.forward;
                break;
            case "inLeft":
                direction = transform.right;
                break;
            case "inRight":
                direction = -transform.right;
                break;
            case "":
                direction = Vector3.zero;
                break;
        }
        imgettingMoved = true;
        float force = 1;
        switch (Mathf.Floor(Mathf.Clamp(timePressed, 0f, 2f)))
        {
            case 0: force = 1.5f; break;
            case 1: force = 2.75f; break;
            case 2: force = 5f; break;
        }
        this.GetComponent<Rigidbody>().AddForce(direction * 300f * force);
        StartCoroutine(BoxIsMoving());
    }
    private IEnumerator RotateToPosition(Quaternion newRotation, float speed)
    {
        float elapsedTime = 0;
        Quaternion startingPos = transform.rotation;
        while (elapsedTime < speed)
        {
            transform.rotation = Quaternion.Lerp(startingPos, newRotation, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator BoxIsMoving()
    {
        yield return new WaitForSeconds(1);
        while (this.GetComponent<Rigidbody>().velocity.magnitude != 0f)
            yield return new WaitForEndOfFrame();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

    }
    private IEnumerator FreezeConstrainsts()
    {
        yield return new WaitForSeconds(1);
        while (this.GetComponent<Rigidbody>().velocity.magnitude != 0f)
            yield return new WaitForEndOfFrame();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
    
