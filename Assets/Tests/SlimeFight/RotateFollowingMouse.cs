using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollowingMouse : MonoBehaviour
{

    void FixedUpdate()
    {
        // ray.origin + t * ray.direction = (x, position.y, z)
        //      1. solve t 
        //      2. solve x and z

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        float y = transform.position.y;
        // if ray does not gonna hit the y=position.y plane, ignore
        if (ray.direction.y == y) return;

        float t = (y - ray.origin.y) / ray.direction.y;
        if (t < 0f) return;

        Vector3 hit = ray.origin + t * ray.direction;
        hit.y = y;
        transform.rotation = Quaternion.LookRotation(hit - transform.position);

        //Debug.Log("look at " + hit);
    }
}
