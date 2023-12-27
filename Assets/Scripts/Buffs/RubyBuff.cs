using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RubyBuff", menuName = "Buff/Ruby Buff")]
public class RubyBuff : Buff
{
    public int increment = 0;

    public override string description
    {
        get
        {
            return ((increment != 0) ? "Ruby " + ToText(increment) + "\n" : "");
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Currency coin = target.GetComponent<Currency>();
        if (coin == null)
        {
            Debug.LogWarning("RubyBuff is applied to No RubyBuff object: " + target.name);
            return;
        }

        coin.ruby += increment;
    }
}
