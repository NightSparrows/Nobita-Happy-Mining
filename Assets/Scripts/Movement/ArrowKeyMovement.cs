using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : ForwardMovement
{
    private float _inputDx = 0;
    private float _inputDz = 0;

    private void Start()
    {
        _normalize = false;
    }
    void Update()
    {
        _inputDx = Input.GetAxis("Horizontal");
        _inputDz = Input.GetAxis("Vertical");
        // Debug.Log(_inputDx + "   " + _inputDz);
        Forward = new Vector3(_inputDx, 0, _inputDz);
    }
}
