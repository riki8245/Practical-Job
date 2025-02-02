﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeControl : MonoBehaviour
{
    [SerializeField] private float slopeAngle;
    [SerializeField] private Transform childPos;
    [SerializeField] private Vector3 slopeNormalV;
    [SerializeField] private float ry;
    //public bool;

    private void Awake()
    {
        childPos = this.gameObject.GetComponentsInChildren<Transform>()[1];
        //ry = this.transform.eulerAngles.y;
        if(this.transform.forward == Vector3.left) ry = 90f;
        else if(this.transform.forward == Vector3.back) ry = 180f;
        else if(this.transform.forward == Vector3.forward) ry = 0f;
        else if(this.transform.forward == Vector3.right) ry = -90f;

            //if (ry == 270f) ry = 90f;
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
        if (other.CompareTag("BoxSlopeInteraction"))
            other.GetComponentInParent<BoxControl>().RotateBox(this.slopeAngle, ry);
        if (other.CompareTag("Player"))
            other.GetComponentInParent<PlayerControl>().onSlope = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoxSlopeInteraction"))
            other.GetComponentInParent<BoxControl>().RotateBox(0f, -1);
        if (other.CompareTag("Player"))
            other.GetComponentInParent<PlayerControl>().onSlope = false;
    }
}
