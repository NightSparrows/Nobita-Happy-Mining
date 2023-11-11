using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Health
{
	private int m_maxHealth;
	private int m_currentHealth;

	public Health(int maxHealth)
	{
		Debug.Assert(maxHealth > 0, "Max health can't be negative!");
		this.m_maxHealth = maxHealth;
		this.m_currentHealth = maxHealth;
	}

	public void takeDamage(int damage)
	{
		Debug.Assert(damage >= 0, "Damage can't be negative!");

		this.m_currentHealth -= damage;
	}

	public bool isDeath()
	{
		return this.m_currentHealth <= 0;
	}

	public int getCurrentHealth() { return this.m_currentHealth; }

	public int getMaxHealth() { return this.m_maxHealth; }

}
