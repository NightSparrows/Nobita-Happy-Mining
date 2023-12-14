using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoveSpeedBuff", menuName = "Buff/Move Speed Buff")]
public class MoveSpeedBuff : FloatAttributeBuff<BaseMovement>
{
    public override string incrementDescription => "Move Speed";
    public override string multiplierIncrementDescription => "Move Speed";
    public override string attributeName => "MoveSpeed";

    public override void ApplyTo(GameObject target)
    {
        BaseMovement[] attrs = target.GetComponents<BaseMovement>();
        if (attrs.Length == 0)
        {
            Debug.LogWarning(attributeName + "Buff is applied to No " + attributeName + " object: " + target.name);
            return;
        }

        foreach (var movement in attrs)
        {
            movement.baseValue += increment;
            movement.valueMultiplier += multiplierIncrement;
        }
    }
}
