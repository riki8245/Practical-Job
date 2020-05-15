using System.Collections;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    public string playerSide;// { get; private set; }
    RaycastHit hit;
    [SerializeField]private bool checkPlayerPosition;
    Vector3 direction;
     

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = this.gameObject;
            checkPlayerPosition = true;
            playerSide = "";
        }
    }
    private void Update()
    {
        if (checkPlayerPosition)
        {
            Vector3 rayPoint = this.transform.position + Vector3.up;
            Debug.DrawRay(rayPoint, transform.forward * 100f, Color.blue);
            Debug.DrawRay(rayPoint, -transform.forward * 100f, Color.blue);
            Debug.DrawRay(rayPoint, transform.right * 100f, Color.blue);
            Debug.DrawRay(rayPoint, -transform.right * 100f, Color.blue);

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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerControl>().box = null;
            this.transform.parent = null;
            playerSide = "";
            checkPlayerPosition = false;
        }
    }

    public void PushBox(float timePressed)
    {
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
        this.GetComponent<Rigidbody>().AddForce(direction * 300f * Mathf.Clamp(timePressed, 0f, 5f));
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
}
