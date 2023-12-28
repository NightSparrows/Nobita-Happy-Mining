using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTargetMovement : BaseMovement
{
    //private Transform target;
    [SerializeField] public Transform target;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target movement's target not found");
            return;
        }
        Vector3 p1 = transform.position;
        Vector3 p2 = target.position;
        Vector3 v = p2 - p1;

        v.y = 0;
        v = v.normalized;

        transform.Translate(v * speed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
