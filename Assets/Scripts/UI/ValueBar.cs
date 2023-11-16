using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * ------------- UI Bars with Animation --------------
 * 
 *      Ref: https://www.youtube.com/watch?v=6U_OZkFtyxY
 *      
 */

public class ValueBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _valueText;

    [SerializeField] private bool _enableText = true;
    [SerializeField] private float _changeSpeed = 1f;

    private int _maxValue = 1;
    private int _value = 1;
    private float _targetRatio = 1;

    public int MaxValue
    {
        get
        {
            return _maxValue;
        }
        set
        {
            _maxValue = value;
            _targetRatio = (float)_value / _maxValue;
        }
    }

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _valueText.text = _value.ToString();
            _targetRatio = (float)_value / _maxValue;
        }
    }

    private void Start()
    {
        _valueText.gameObject.SetActive(_enableText);
        MaxValue = 10;
        Value = 10;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Value -= 1;
        }
        UpdateValue();
    }

    private void UpdateValue()
    {
        _fill.fillAmount = Mathf.MoveTowards(_fill.fillAmount, _targetRatio, _changeSpeed * Time.deltaTime);
    }
}
