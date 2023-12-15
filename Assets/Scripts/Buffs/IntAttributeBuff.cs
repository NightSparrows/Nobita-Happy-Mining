using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IntAttributeBuff<T> : Buff where T : IntAttribute
{
    public abstract string incrementDescription { get; }
    public abstract string multiplierIncrementDescription { get; }
    public abstract string attributeName { get; }

    public int increment = 0;
    public float multiplierIncrement = 0f;

    public override string description
    {
        get
        {
            return
                ((increment != 0) ? incrementDescription + " " + ToText(increment) + "\n" : "") +
                ((multiplierIncrement != 0f) ? multiplierIncrementDescription + " " + ToText(multiplierIncrement * 100) + "%\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        T attr = target.GetComponent<T>();
        if (attr == null)
        {
            Debug.LogWarning(attributeName + "Buff is applied to No " + attributeName + " object: " + target.name);
            return;
        }

        attr.baseValue += increment;
        attr.valueMultiplier += multiplierIncrement;
    }
}