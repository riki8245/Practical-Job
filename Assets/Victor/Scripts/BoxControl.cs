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
    const float groundRadius = .1f;
    public Transform [] rayCasts = new Transform[4];

    private void Start()
    {
        StartCoroutine(BoxIsMoving());
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
        if (checkPlayerPosition)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayCasts[3].position, transform.forward, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inFront";
            else if (Physics.Raycast(rayCasts[2].position, -transform.forward, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inBack";
            else if (Physics.Raycast(rayCasts[1].position, -transform.right, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inLeft";
            else if (Physics.Raycast(rayCasts[0].position, transform.right, out hit, 100f, LayerMask.GetMask("Player")))
                playerSide = "inRight";
            else
                playerSide = "";
            /*Debug.DrawRay(rayCasts[3].position, transform.forward * 100, Color.blue);
            Debug.DrawRay(rayCasts[2].position, -transform.forward * 100, Color.black);
            Debug.DrawRay(rayCasts[0].position, -transform.right * 100, Color.yellow);
            Debug.DrawRay(rayCasts[1].position, transform.right * 100, Color.red);*/
        }
        if (this.gameObject.layer == 8 && !imgettingMoved)
        {
            Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundRadius);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            foreach (Collider item in hitColliders) if (item.gameObject.layer == 9) this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        outline.SetActive(showOutline && !imgettingMoved);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = this.gameObject;
            checkPlayerPosition = true;
            playerSide = "";
            showOutline = true;
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
            showOutline = false;

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
        imgettingMoved = false;


    }
}
    
