using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Currency : MonoBehaviour
{
    [SerializeField] private int _ruby = 0;

    public event Action<int, int> OnRubyChanged;
    public event Action<int, int> OnDiamonaChanged;

    public int ruby
    {
        get
        {
            return _ruby;
        }
        set
        {
            if (_ruby == value) return;
            int org = _ruby;
            _ruby = value;
            OnRubyChanged?.Invoke(org, value);
        }
    }
    public int diamond
    {
        get
        {
            return PlayerPrefs.GetInt("Diamond", 0);
        }
        set
        {
            int org = PlayerPrefs.GetInt("Diamond", 0);
            if (org == value) return;
            PlayerPrefs.SetInt("Diamond", value);
            OnDiamonaChanged?.Invoke(org, value);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 0);
        }
    }
}
