using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapResolver : MonoBehaviour
{
    private Rigidbody rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        rig.velocity = Vector3.zero;
    }
}
