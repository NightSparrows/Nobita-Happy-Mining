using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class OptionButton : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] private TextMeshProUGUI buffDescription;
    [SerializeField] private TextMeshProUGUI owner;

    public string ownerText
    {
        set
        {
            owner.text = value;
        }
    }

    public string buffText
    {
        set
        {
            buffDescription.text = value;
        }
    }

    public void ClickCallback()
    {
        OnClick?.Invoke();
    }
}
