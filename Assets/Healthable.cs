using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthable : MonoBehaviour
{

    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;

    public void InitializeHealth(int mx) {
        maxHealth = mx;
        health = mx;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }
}
