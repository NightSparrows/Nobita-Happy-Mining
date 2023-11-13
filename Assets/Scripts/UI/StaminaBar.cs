using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    private Stamina staminaListen;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnDestroy()
    {
        staminaListen.OnMaxStaminaChanged -= UpdateMaxStamina;
        staminaListen.OnStaminaChanged -= UpdateStamina;
    }

    private void Start()
    {
        staminaListen = GameManager.Instance.player.GetComponent<Stamina>();
        staminaListen.OnMaxStaminaChanged += UpdateMaxStamina;
        staminaListen.OnStaminaChanged += UpdateStamina;

        UpdateMaxStamina(staminaListen.MaxStamina);
        UpdateStamina(staminaListen.CurrentStamina);
    }

    private void UpdateMaxStamina(int newMaxStamina)
    {
        slider.maxValue = newMaxStamina;
    }

    private void UpdateStamina(int newStamina)
    {
        slider.value = newStamina;
    }
}
