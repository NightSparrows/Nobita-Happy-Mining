using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    [SerializeField] int armor;
    [SerializeField] float damageReduceRate;

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
