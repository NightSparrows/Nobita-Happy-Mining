using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    [SerializeField] public int BaseDamageIncrement { get; set; } = 0;
    [SerializeField] public float DamageIncrement { get; set; } = 0.0f;
    [SerializeField] public float CriticalChanceIncrement { get; set; } = 0.0f;
    [SerializeField] public float CriticalDamageIncrement { get; set; } = 0.0f;

    public void BuffTo(Attack target)
    {
        target.BaseDamage += BaseDamageIncrement;
        target.DamageMultiplier += DamageIncrement;
        target.CriticalChance += CriticalChanceIncrement;
        target.CriticalDamageMultiplier += CriticalDamageIncrement;
    }
}
