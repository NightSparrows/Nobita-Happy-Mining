using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int _maxExp;
    private int _exp = 0;
    [SerializeField] private int _level;

    public int MaxPlayerExp
    {
        get
        {
            return _maxExp;
        }

        set
        {
            _maxExp = value;
            OnMaxPlayerExpChanged?.Invoke(value);
        }
    }

    public int CurrentPlayerExp
    {
        get
        {
            return _exp;
        }

        set
        {
            _exp = value;
            OnPlayerExpChanged?.Invoke(value);
        }
    }

    public int PlayerLevel
    {
        get
        {
            return _level;
        }
    }

    public event Action<int> OnMaxPlayerExpChanged;
    public event Action<int> OnPlayerExpChanged;
    public event Action<int> OnPlayerLevelChanged;
}
