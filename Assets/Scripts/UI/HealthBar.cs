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
    }

    private void OnDestroy()
    {
        healthListen.OnMaxHealthChanged -= UpdateMaxHealth;
        healthListen.OnHealthChanged -= UpdateHealth;
    }

    private void Start()
    {
        healthListen = GameManager.Instance.player.GetComponent<Health>();
        healthListen.OnMaxHealthChanged += UpdateMaxHealth;
        healthListen.OnHealthChanged += UpdateHealth;

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
