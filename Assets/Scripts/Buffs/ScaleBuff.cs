using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScaleBuff", menuName = "Buff/Scale Buff")]
public class ScaleBuff : Buff
{
    public float multiplier = 1f;

    public override string description
    {
        get
        {
            //Debug.Log("BulletBuff GenerateDescription");
            return string.Format("Size {0}%\n", ToText(multiplier * 100));
        }
    }

    public override void ApplyTo(GameObject target)
    {
        Vector3 org = target.transform.localScale;
        target.transform.localScale = org * multiplier;
    }
}
