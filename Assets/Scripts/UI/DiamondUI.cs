using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondUI : MonoBehaviour
{
    [SerializeField] Currency currency;
    [SerializeField] TextMeshProUGUI text;

    void OnEnable()
    {
        currency.OnDiamondChanged += OnDiamondChanged;
        text.text = currency.diamond.ToString();
    }

    void OnDisable()
    {
        currency.OnDiamondChanged -= OnDiamondChanged;
    }

    void OnDiamondChanged(int org, int cur)
    {
        text.text = cur.ToString();
    }
}
