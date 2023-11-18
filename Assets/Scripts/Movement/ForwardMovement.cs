using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : BaseMovement
{
    [SerializeField] private Vector3 _forward = Vector3.zero;
    public Vector3 Forward
    {
        get
        {
            return _forward;
        }
        set
        {
            _forward = value;
            if (_normalize && _forward != Vector3.zero)
            {
                _forward = value.normalized;
            }
        }
    }
    [SerializeField] protected bool _normalize = true;

    private void FixedUpdate()
    {
        transform.Translate(Forward * Speed * Time.deltaTime);

        IsMovingInThisTick = Forward != Vector3.zero && Speed != 0;
        HandleMoveEvents();
    }
}
