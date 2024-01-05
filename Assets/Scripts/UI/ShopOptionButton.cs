using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ShopOptionButton : MonoBehaviour
{
    public event Action OnClick;

    [SerializeField] private TextMeshProUGUI buffDescription;
    [SerializeField] private TextMeshProUGUI owner;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Color priceBuyableColor;
    [SerializeField] private Color priceUnbuyableColor;

    [SerializeField] private Image _currencyImage;
    [SerializeField] private Button button;

    public bool buyable
    {
        set
        {
            button.interactable = value;
            priceText.color = value ? priceBuyableColor : priceUnbuyableColor;
        }
    }

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

    public int price
    {
        set
        {
            priceText.text = value.ToString();
        }
    }

    public Sprite currencyImage
    {
        set
        {
            _currencyImage.sprite = value;
        }
    }

    public void ClickCallback()
    {
        OnClick?.Invoke();
    }
}
