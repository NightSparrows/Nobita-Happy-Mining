using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Transform _camTransform;

    private void Start()
    {
        if (_camTransform == null)
            _camTransform = Camera.main.transform;
    }

    private void Update()
    {
        //transform.LookAt(_camTransform.position);
        //transform.rotation = Quaternion.LookRotation(transform.position - _camTransform.position);
        transform.forward = _camTransform.forward;
    }
}
