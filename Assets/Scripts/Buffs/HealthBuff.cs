using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealthBuff", menuName = "Buff/Health Buff")]
public class HealthBuff : Buff
{
    public int healthIncrement = 0;
    public int maxHealthIncrement = 0;

    public override string description
    {
        get
        {
            return "Health +" + healthIncrement;
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Health hp = target.GetComponent<Health>();
        if (hp == null)
        {
            Debug.LogWarning("HealthBuff is applied to Non-attackable object:" + target.name);
            return;
        }
        Debug.Log("heakth!");

        hp.MaxHealth += maxHealthIncrement;
        hp.CurrentHealth += healthIncrement;

    }
}
