using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health healthListen;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        healthListen = GameManager.Instance.player.GetComponent<Health>();
        healthListen.OnMaxHealthChanged += UpdateMaxHealth;
        healthListen.OnHealthChanged += UpdateHealth;
    }

    private void Start()
    {
        UpdateMaxHealth(healthListen.MaxHealth);
        UpdateHealth(healthListen.CurrentHealth);
    }

    private void UpdateMaxHealth(int newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }

    private void UpdateHealth(int newHealth)
    {
        slider.value = newHealth;
    }
}
