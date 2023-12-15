using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New FloatAttributeBuff", menuName = "Buff/Float Attribute Buff")]
public abstract class FloatAttributeBuff<T> : Buff where T : FloatAttribute
{
    public abstract string incrementDescription { get; }
    public virtual string incrementDescriptionPostFix => "";
    public abstract string multiplierIncrementDescription { get; }
    public virtual string multiplierIncrementDescriptionPostfix => "%";
    public abstract string attributeName { get; }

    public float increment = 0;
    public float multiplierIncrement = 0f;

    public override string description
    {
        get
        {
            return
                ((increment != 0f) ? incrementDescription + " " + ToText(increment) + incrementDescriptionPostFix + "\n" : "") +
                ((multiplierIncrement != 0f) ? multiplierIncrementDescription + " " + ToText(multiplierIncrement * 100) + multiplierIncrementDescriptionPostfix + "\n" : "");
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