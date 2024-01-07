using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubyUI : MonoBehaviour
{
    [SerializeField] Currency currency;
    [SerializeField] TextMeshProUGUI text;

    void OnEnable()
    {
        currency.OnRubyChanged += OnRubyChanged;
        text.text = currency.ruby.ToString();
    }

    void OnDisable()
    {
        currency.OnRubyChanged -= OnRubyChanged;
    }

    void OnRubyChanged(int org, int cur)
    {
        text.text = cur.ToString();
    }
}
