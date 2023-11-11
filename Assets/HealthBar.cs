using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
    }

    // public void LateUpdate()
    // {
    //     SetMaxHealth(listen.GetMaxHealth());
    //     UpdateHealth(listen.GetHealth());
    // }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth >= 0? maxHealth : 0;
    }

    public void UpdateHealth(int health)
    {
        slider.value = health >= 0? health : 0;
    }
}
