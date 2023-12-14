using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : ForwardMovement
{
    //private Transform target;
    [SerializeField] public Transform target;
    [SerializeField] private float rotateSpeed = 80;

    protected override void FixedUpdate()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = target.position;
        Vector3 v = p2 - p1;

        v.y = 0;

        //Forward = v;
        //Debug.Log(transform.position + ", " + target.position);
        //Debug.Log("Dis = " + v);
        transform.rotation = Quaternion.LookRotation(v);
        base.FixedUpdate();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
