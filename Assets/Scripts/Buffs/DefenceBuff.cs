using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DefenceBuff", menuName = "Buff/Defence Buff")]
public class DefenceBuff : Buff
{
    public int armorIncrement = 0;
    public float damageReduceRateIncrement = 0.0f;

    public override string description
    {
        get
        {
            return
                ((armorIncrement != 0) ? "Armor " + ToText(armorIncrement) + "\n" : "") +
                ((damageReduceRateIncrement != 0) ? "Damage Reduce " + ToText(damageReduceRateIncrement * 100) + "%\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Defense def = target.GetComponent<Defense>();
        if (def == null)
        {
            Debug.LogWarning("DefenceBuff is applied to No Defense object: " + target.name);
            return;
        }

        def.armor += armorIncrement;
        def.damageReduceRate += damageReduceRateIncrement;
    }
}

