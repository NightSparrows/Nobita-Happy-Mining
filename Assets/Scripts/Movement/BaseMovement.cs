using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] protected float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    public event Action OnMove, OnMoveStart, OnMoveEnd;

    private bool _isMovingInLastTick = false;
    private bool _isMovingInThisTick = false;

    protected bool IsMovingInThisTick
    {
        get
        {
            return _isMovingInThisTick;
        }
        set
        {
            _isMovingInThisTick = value;
        }
    }

    protected void HandleMoveEvents()
    {
        if (!_isMovingInLastTick && _isMovingInThisTick) OnMoveStart?.Invoke();
        if (_isMovingInThisTick) OnMove?.Invoke();
        if (_isMovingInLastTick && !_isMovingInThisTick) OnMoveEnd?.Invoke();
        _isMovingInLastTick = _isMovingInThisTick;
    }
}
