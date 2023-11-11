using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Healthable listen;

    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        
        int maxHealth = listen.GetMaxHealth();
        SetMaxHealth(maxHealth);
        UpdateHealth(maxHealth);
    }

    public void LateUpdate()
    {
        UpdateHealth(listen.GetHealth());
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void UpdateHealth(int health)
    {
        slider.value = health;
    }
}
