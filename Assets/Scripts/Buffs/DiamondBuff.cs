using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DiamondBuff", menuName = "Buff/Diamond Buff")]
public class DiamondBuff : Buff
{
    public int increment = 0;

    public override string description
    {
        get
        {
            return ((increment != 0) ? "Diamond " + ToText(increment) + "\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Currency coin = target.GetComponent<Currency>();
        if (coin == null)
        {
            Debug.LogWarning("DiamondBuff is applied to No DiamondBuff object: " + target.name);
            return;
        }

        coin.diamond += increment;
    }
}
