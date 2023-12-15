using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulletSpreadBuff", menuName = "Buff/Bullet Spread Buff")]
public class BulletSpreadBuff : FloatAttributeBuff<BulletSpread>
{
    public override string incrementDescription => "Bullet Spread";
    public override string incrementDescriptionPostFix => " degree";
    public override string multiplierIncrementDescription => "Bullet Spread";
    public override string attributeName => "BulletSpread";
}
