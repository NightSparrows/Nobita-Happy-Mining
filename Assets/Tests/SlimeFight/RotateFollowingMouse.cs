using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollowingMouse : MonoBehaviour
{

    void FixedUpdate()
    {
        // ray.origin + t * ray.direction = (x, 0, z)
        //      1. solve t 
        //      2. solve x and z

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if ray does not gonna hit the y=0 plane, ignore
        if (ray.direction.y == 0) return;

        float t = -ray.origin.y / ray.direction.y;
        if (t < 0f) return;

        Vector3 hit = ray.origin + t * ray.direction;
        hit.y = 0f;
        transform.rotation = Quaternion.LookRotation(hit - transform.position);

        //Debug.Log("look at " + hit);
    }
}
