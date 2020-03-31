using UnityEngine;

public class SlopeInfo : MonoBehaviour
{
    private float slopeAngle;
    private Transform childPos;
    private Vector3 slopeNormalV;

    void Start()
    {
        childPos = GetComponentInChildren<Transform>();
        RaycastHit outRay;
        if (Physics.Raycast(this.childPos.position, -Vector3.up * 10, out outRay))
            slopeNormalV = outRay.normal;
        getAngle(slopeNormalV);
    }
    private void getAngle(Vector3 normal)
    {
        Vector3 vec1 = Vector3.up;
        Vector3 vec2 = normal;
        float dot = Vector3.Dot(vec1, vec2);
        dot /= (vec1.magnitude * vec2.magnitude);
        float acos = Mathf.Acos(dot);
        slopeAngle = acos * 180 / Mathf.PI;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
            other.GetComponent<BoxController>().RotateBox(this.slopeAngle,slopeNormalV);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
            other.GetComponent<BoxController>().RotateBox(0f, Vector3.up);
    }
}
