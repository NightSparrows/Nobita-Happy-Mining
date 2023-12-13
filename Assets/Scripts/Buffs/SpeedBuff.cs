using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedBuff", menuName = "Buff/Speed Buff")]
public class SpeedBuff : Buff
{
    public float multiplier = 1f;

    public override string description
    {
        get
        {
            //Debug.Log("BulletBuff GenerateDescription");
            return string.Format("Speed {0}%\n", ToText(multiplier * 100));
        }
    }

    public override void ApplyTo(GameObject target)
    {
        BaseMovement move = target.GetComponent<BaseMovement>();
        if (move == null)
        {
            Debug.LogWarning("SpeedBuff is applied to Non-movable object: " + target.name);
            return;
        }

        move.speedMultiplier += multiplier;
    }
}
