using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private int _maxExp;
    private int _exp = 0;
    [SerializeField] private int _level;

    [SerializeField] public LevelUpSystem levelUpSystem { get; set; }

    public int MaxPlayerExp
    {
        get
        {
            return _maxExp;
        }

        set
        {
            if (_maxExp == value) return;
            _maxExp = value;
            OnMaxPlayerExpChanged?.Invoke(value);
            if (_exp >= _maxExp) LevelUp();
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
            if (_exp == value) return;
            _exp = value;
            OnPlayerExpChanged?.Invoke(value);
            if (_exp >= _maxExp) LevelUp();
        }
    }

    public int PlayerLevel
    {
        get
        {
            return _level;
        }

        set
        {
            if (_level == value) return;
            _level = value;
            OnPlayerLevelChanged?.Invoke(value);
        }
    }

    private void LevelUp()
    {
        _exp -= _maxExp;
        _maxExp = levelUpSystem.ExpRequired(_level + 1);
        OnPlayerExpChanged?.Invoke(_exp);
        OnMaxPlayerExpChanged?.Invoke(_maxExp);
        ++PlayerLevel;
    }

    public event Action<int> OnMaxPlayerExpChanged;
    public event Action<int> OnPlayerExpChanged;
    public event Action<int> OnPlayerLevelChanged;
}
