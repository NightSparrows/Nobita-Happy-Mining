using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    [field: SerializeField] public int armor { get; set; } = 0;
    [field: SerializeField] public float damageReduceRate { get; set; } = 0f;

    Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }


    public void BeAttacked(in Attack attack)
    {
        // TODO:invoke
        int damage = attack.Damage;
        // TODO:defense
        health.takeDamage(damage);
    }
}
