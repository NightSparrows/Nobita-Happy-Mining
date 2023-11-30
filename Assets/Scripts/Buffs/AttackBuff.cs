using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AttackBuff", menuName = "Buff/Attack Buff")]
public class AttackBuff : Buff
{
    public int baseDamageIncrement = 0;
    public float damageIncrement = 0.0f;
    public float criticalChanceIncrement = 0.0f;
    public float criticalDamageIncrement = 0.0f;

    public override string description
    {
        get
        {
            Debug.Log("AttackBuff GenerateDescription");
            return
                ((baseDamageIncrement != 0) ? "Base Damage " + ToText(baseDamageIncrement) + "\n" : "") +
                ((damageIncrement != 0) ? "Damage " + ToText(damageIncrement * 100) + "%\n" : "") +
                ((criticalChanceIncrement != 0) ? "Critical Change " + ToText(criticalChanceIncrement) + "\n" : "") +
                ((criticalDamageIncrement != 0) ? "Critical Damage " + ToText(criticalDamageIncrement * 100) + "%\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Attack atk = target.GetComponent<Attack>();
        if (atk == null)
        {
            Debug.LogWarning("AttackBuff is applied to Non-attackable object: " + target.name);
            return;
        }

        atk.BaseDamage += baseDamageIncrement;
        atk.DamageMultiplier += damageIncrement;
        atk.CriticalChance += criticalChanceIncrement;
        atk.CriticalDamageMultiplier += criticalDamageIncrement;
    }
}
