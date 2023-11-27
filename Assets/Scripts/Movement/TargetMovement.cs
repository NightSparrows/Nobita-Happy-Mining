using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : ForwardMovement
{
    //private Transform target;
    [SerializeField] Transform target;

    protected override void FixedUpdate()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = target.position;
        Vector3 v = p2 - p1;

        v.y = 0;

        Forward = v;
        base.FixedUpdate();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
