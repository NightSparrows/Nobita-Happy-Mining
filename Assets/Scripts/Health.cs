using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Health : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;

	public void init(int maxHealth)
	{
		Debug.Assert(maxHealth > 0, "Max health can't be negative!");
		this.maxHealth = maxHealth;
		this.currentHealth = maxHealth;
	}

	public void takeDamage(int damage)
	{
		Debug.Assert(damage >= 0, "Damage can't be negative!");

		this.currentHealth -= damage;
	}

	public void heal(int heal)
	{
		this.currentHealth += heal;
		if (this.currentHealth > this.maxHealth)
		{
			this.currentHealth = this.maxHealth;
		}
	}

	public bool isDeath()
	{
		return this.currentHealth <= 0;
	}

	public int getCurrentHealth() { return this.currentHealth; }

	public int getMaxHealth() { return this.maxHealth; }

}
