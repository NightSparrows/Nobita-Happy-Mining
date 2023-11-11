using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject listen;
    Health healthListen;
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        healthListen = listen.GetComponent<Health>();
    }

    public void LateUpdate()
    {
        SetMaxHealth(healthListen.getMaxHealth());
        UpdateHealth(healthListen.getCurrentHealth());
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth >= 0? maxHealth : 0;
    }

    public void UpdateHealth(int health)
    {
        slider.value = health >= 0? health : 0;
    }
}
