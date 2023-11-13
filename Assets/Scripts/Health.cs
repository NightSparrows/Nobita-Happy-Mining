using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Health : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;

	public event Action<int> OnMaxHealthChanged;
	public event Action<int> OnHealthChanged;

	public int MaxHealth
    {
		get
        {
			return maxHealth;
        }
    }

	public int CurrentHealth
    {
		get
        {
			return currentHealth;
        }
    }

	public void init(int maxHealth)
	{
		Debug.Assert(maxHealth > 0, "Max health can't be negative!");
		UpdateMaxHealth(maxHealth);
		UpdateHealth(maxHealth);
	}

	public void takeDamage(int damage)
	{
		Debug.Assert(damage >= 0, "Damage can't be negative!");

		UpdateHealth(currentHealth - damage);
	}

	public void heal(int heal)
	{
		this.currentHealth += heal;
		if (this.currentHealth > this.maxHealth)
		{
			UpdateHealth(maxHealth);
		}
	}

	public bool isDead()
	{
		return this.currentHealth <= 0;
	}

	private void UpdateHealth(int newHealth)
    {
		currentHealth = newHealth;
		OnHealthChanged?.Invoke(newHealth);
    }

	private void UpdateMaxHealth(int newMaxHealth)
    {
		maxHealth = newMaxHealth;
		OnMaxHealthChanged?.Invoke(newMaxHealth);
    }

	// public int getCurrentHealth() { return this.currentHealth; }

	// public int getMaxHealth() { return this.maxHealth; }

}
