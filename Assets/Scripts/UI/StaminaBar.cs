using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Stamina _listen; // set to Player in default
    [SerializeField] private ValueBar _valueBar;

    private void Start()
    {
        if (_listen == null)
        {
            _listen = GameManager.Instance.player.GetComponent<Stamina>();
        }
        _listen.OnMaxStaminaChanged += UpdateMaxValue;
        _listen.OnStaminaChanged += UpdateValue;

        UpdateMaxValue(_listen.MaxStamina);
        UpdateValue(_listen.CurrentStamina);
    }

    private void OnDestroy()
    {
        _listen.OnMaxStaminaChanged -= UpdateMaxValue;
        _listen.OnStaminaChanged -= UpdateValue;
    }

    private void UpdateMaxValue(int newMaxValue)
    {
        _valueBar.MaxValue = newMaxValue;
    }

    private void UpdateValue(int newValue)
    {
        _valueBar.Value = newValue;
    }
}