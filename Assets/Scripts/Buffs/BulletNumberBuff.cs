using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulletNumberBuff", menuName = "Buff/Bullet Number Buff")]
public class BulletNumberBuff : IntAttributeBuff<BulletNumber>
{
    public override string incrementDescription => "BulletNumber";
    public override string multiplierIncrementDescription => "Bullet Number";
    public override string attributeName => "BulletNumber";
}
