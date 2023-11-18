using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    [field: SerializeField] public int BaseDamageIncrement { get; set; } = 0;
    [field: SerializeField] public float DamageIncrement { get; set; } = 0.0f;
    [field: SerializeField] public float CriticalChanceIncrement { get; set; } = 0.0f;
    [field: SerializeField] public float CriticalDamageIncrement { get; set; } = 0.0f;

    public void BuffTo(Attack target)
    {
        target.BaseDamage += BaseDamageIncrement;
        target.DamageMultiplier += DamageIncrement;
        target.CriticalChance += CriticalChanceIncrement;
        target.CriticalDamageMultiplier += CriticalDamageIncrement;
    }
}
