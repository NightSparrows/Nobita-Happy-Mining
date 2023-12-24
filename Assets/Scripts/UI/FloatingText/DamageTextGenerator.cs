using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageTextGenerator : FloatingTextGenerator
{
    public Vector3 offset = Vector3.zero;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int orgHealth, int newHealth)
    {
        int damage = orgHealth - newHealth;
        if (damage <= 0) return; // not taken damage

        Generate(transform.position + offset, damage);
    }
}
