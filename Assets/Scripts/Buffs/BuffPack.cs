using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BuffPack", menuName = "Buff/Buff Pack")]
public class BuffPack : Buff
{
    public Buff[] buffs;

    public override string description
    {
        get
        {
            Debug.Log("BuffPack GenerateDescription");
            string des = string.Empty;
            foreach (var buff in buffs)
            {
                des += buff.description;
            }
            return des;
        }
    }

    public override void ApplyTo(GameObject target)
    {
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i].ApplyTo(target);
        }   
    }
}
