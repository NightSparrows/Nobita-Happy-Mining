using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerExpBuff", menuName = "Buff/PlayerExp Buff")]
public class PlayerExpBuff : Buff
{
    public int expIncrement = 0;

    public override string description
    {
        get
        {
            Debug.Log("AttackBuff GenerateDescription");
            return
                ((expIncrement != 0) ? "Exp " + ToText(expIncrement) + "\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        PlayerExperience exp = target.GetComponent<PlayerExperience>();
        if (exp == null)
        {
            Debug.LogWarning("PlayerExpBuff is applied to No Exp object: " + target.name);
            return;
        }

        exp.CurrentPlayerExp += expIncrement;
    }
}
