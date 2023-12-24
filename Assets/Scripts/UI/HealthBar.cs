using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _listen; // set to Player in default
    [SerializeField] private ValueBar _valueBar;

    private void Start()
    {
        if (_listen == null)
        {
            if (GameManager.Instance.player != null)
                _listen = GameManager.Instance.player.GetComponent<Health>();
            else
                Debug.LogError("The target of Health Bar is missing!");
        }
        _listen.OnMaxHealthChanged += UpdateMaxValue;
        _listen.OnHealthChanged += UpdateValue;

        UpdateMaxValue(0, _listen.MaxHealth);
        UpdateValue(0, _listen.CurrentHealth);
    }

    private void OnDestroy()
    {
        _listen.OnMaxHealthChanged -= UpdateMaxValue;
        _listen.OnHealthChanged -= UpdateValue;
    }

    private void UpdateMaxValue(int orgMaxValue, int newMaxValue)
    {
        _valueBar.MaxValue = newMaxValue;
    }

    private void UpdateValue(int orgValue, int newValue)
    {
        _valueBar.Value = newValue;
    }
}
