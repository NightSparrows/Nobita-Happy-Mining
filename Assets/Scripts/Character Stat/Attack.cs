using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [field: SerializeField] public int BaseDamage { get; set; } = 0;
    [field: SerializeField] public float DamageMultiplier { get; set; } = 1.0f;
    [field: SerializeField] public float CriticalChance { get; set; } = 0.0f;
    [field: SerializeField] public float CriticalDamageMultiplier { get; set; } = 1.0f;

    public int Damage
    {
        get
        {
            int damage = (int)(BaseDamage * DamageMultiplier);
            if (Random.value < CriticalChance)
            {
                damage = (int)(damage * CriticalDamageMultiplier);
            }
            return damage;
        }
    }
}
