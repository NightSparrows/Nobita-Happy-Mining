using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject listen;
    Health healthListen;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        listen = GameManager.Instance.player;
        healthListen = listen.GetComponent<Health>();

        healthListen.OnHealthChanged += UpdateHealth;
        healthListen.OnMaxHealthChanged += UpdateMaxHealth;
    }

    public void UpdateHealth(int newHealth)
    {
        slider.value = newHealth;
    }

    private void UpdateMaxHealth(int newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }
}
