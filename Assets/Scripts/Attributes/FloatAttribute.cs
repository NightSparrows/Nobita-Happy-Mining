using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAttribute : MonoBehaviour
{
    //public event Action<float> OnCoolDownChanged;

    [SerializeField] private float _baseValue = 0;
    [SerializeField] private float _valueMultiplier = 1f;

    public float value
    {
        get
        {
            return _baseValue * _valueMultiplier;
        }
    }

    public float baseValue
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
