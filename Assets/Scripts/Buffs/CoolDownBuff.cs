using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CoolDownBuff", menuName = "Buff/CoolDown Buff")]
public class CoolDownBuff : FloatAttributeBuff<CoolDown>
{
    public override string incrementDescription => "Cool Down";
    public override string incrementDescriptionPostFix => "s";
    public override string multiplierIncrementDescription => "Cool Down";
    public override string attributeName => "CoolDown";
}
