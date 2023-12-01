using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseMovement : FloatAttribute
{
    public virtual float speed
    {
        get
        {
            return this.value;
        }
    }

    public virtual float baseSpeed
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = value;
        }
    }

    public virtual float speedMultiplier
    {
        get
        {
            return valueMultiplier;
        }
        set
        {
            valueMultiplier = value;
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
