using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health healthListen;
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
        if (healthListen == null)
        {
            if (GameManager.Instance.player != null)
                healthListen = GameManager.Instance.player.GetComponent<Health>();
            else
                Debug.LogError("The target of Health Bar is missing!");
        }
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
