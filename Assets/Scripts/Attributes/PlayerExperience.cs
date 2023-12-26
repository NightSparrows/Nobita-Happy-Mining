using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int _maxExp;
    [SerializeField] private int _exp = 0;
    [SerializeField] private int _level = 1;

    [field: SerializeField] public LevelUpSystem levelUpSystem { get; set; }

    private void Awake()
    {
        _maxExp = levelUpSystem.ExpRequired(_level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentPlayerExp += MaxPlayerExp - CurrentPlayerExp;
        }
    }

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
        ++PlayerLevel;
        OnPlayerExpChanged?.Invoke(_exp);
        OnMaxPlayerExpChanged?.Invoke(_maxExp);
    }

    public event Action<int> OnMaxPlayerExpChanged;
    public event Action<int> OnPlayerExpChanged;
    public event Action<int> OnPlayerLevelChanged;
}
