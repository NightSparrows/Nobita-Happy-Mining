using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuffPack", menuName = "Buff/Buff Pack")]
public class BuffPack : Buff
{
    public Buff[] buffs;

    public override void ApplyTo(GameObject target)
    {
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i].ApplyTo(target);
        }   
    }
}
