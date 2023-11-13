using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private int _maxStamina;
    private int _stamina;

    private void Awake()
    {
        _stamina = _maxStamina;
    }

    public int MaxStamina
    {
        get
        {
            return _maxStamina;
        }

        set
        {
            _maxStamina = value;
            OnMaxStaminaChanged?.Invoke(value);
        }
    }

    public int CurrentStamina
    {
        get
        {
            return _stamina;
        }

        set
        {
            _stamina = value;
            OnStaminaChanged?.Invoke(value);
        }
    }

    public event Action<int> OnMaxStaminaChanged;
    public event Action<int> OnStaminaChanged;
}
