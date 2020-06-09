using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MS_enemyMove : MonoBehaviour
{
    private NavMeshAgent e_nav;
    private Rigidbody e_rb;
    private Animator e_anim;
    private int e_GoTo;
    public Transform pointA;
    public Transform pointB;

    private Vector3 e_PointA;
    private Vector3 e_PointB;

    private void Awake()
    {
        e_nav = GetComponent<NavMeshAgent>();
        e_rb = GetComponent<Rigidbody>();
        e_anim = GetComponent<Animator>();
        e_GoTo = 1;
        e_PointA = pointA.position;
        e_PointB = pointB.position;
    }
    private void OnEnable()
    {
        e_nav.enabled = true;
        e_nav.SetDestination(e_PointB);
    }
    void Update()
    {
        if (Vector3.Distance(this.transform.position, e_PointA) < 1f && e_GoTo == 0)
        {
            e_nav.enabled = false;
            iTween.RotateTo(this.gameObject, iTween.Hash("y", 0f, "oncomplete", "changeTargetPoint"));
        }
        else if (Vector3.Distance(this.transform.position, e_PointB) < 1f && e_GoTo == 1) {
            e_nav.enabled = false;
            iTween.RotateTo(this.gameObject, iTween.Hash("y", 180f, "oncomplete", "changeTargetPoint"));
        }
        e_anim.SetFloat("Speed", e_nav.velocity.magnitude);
    }
    private void changeTargetPoint()
    {
        e_nav.enabled = true;
        e_nav.isStopped = true;
        if (e_GoTo == 0) {
            e_GoTo = 1; e_nav.SetDestination(e_PointB);
        } 
        else {
            e_GoTo = 0; e_nav.SetDestination(e_PointA);
        }
        e_nav.isStopped = false;

    }
}
