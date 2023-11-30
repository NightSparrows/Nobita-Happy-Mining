using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{

    public abstract void ApplyTo(GameObject target);
    public virtual void RemoveFrom(GameObject target) { }

    protected string ToText(int value) 
    {
        if (value >= 0) return "+" + value;
        return value.ToString();
    }

    protected string ToText(float value)
    {
        if (value >= 0) return "+" + value;
        return value.ToString();
    }

    public virtual string description 
    {
        get;
    }
}
