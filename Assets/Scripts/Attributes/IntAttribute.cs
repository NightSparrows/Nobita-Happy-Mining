using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntAttribute : MonoBehaviour
{
    //public event Action<float> OnCoolDownChanged;

    [SerializeField] private int _baseValue = 0;
    [SerializeField] private float _valueMultiplier = 1f;

    public int value
    {
        get
        {
            return (int)(_baseValue * _valueMultiplier);
        }
    }

    public int baseValue
    {
        get
        {
            return _baseValue;
        }
        set
        {
            if (_baseValue == value) return;
            _baseValue = value;
            //OnCoolDownChanged.Invoke(coolDown);
        }
    }
    public float valueMultiplier
    {
        get
        {
            return _valueMultiplier;
        }
        set
        {
            if (_valueMultiplier == value) return;
            _valueMultiplier = value;
            //OnCoolDownChanged(coolDown);
        }
    }
}
