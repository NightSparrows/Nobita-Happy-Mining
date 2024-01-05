using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionButtomPrefab;
    [SerializeField] private Transform optionContainer;

    [SerializeField] private float gapOfChoices = 100f;

    [SerializeField] private Sprite[] rubyImages;
    [SerializeField] private int[] rubyImageRange;

    public event Action OnShoppingEnd;

    private float buttonWidth;

    private List<GameObject> optionButtons;

    private void Awake()
    {
        buttonWidth = optionButtomPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    // called by UpgradeManager, where the choices are given
    public void ShowChoices(List<(Upgrade, int)> choices, ShopConsumer consumer, int budget)
    {
        //Debug.Log("Choice count= " + choices.Count);
        optionButtons = new List<GameObject>();

        int n = choices.Count;

        for (int i = 0; i < n; ++i)
        {
            GameObject button = Instantiate(optionButtomPrefab, optionContainer);

            // setup button info
            button.SetActive(false);
            float dx = GetOffsetX(i, n);
            button.transform.localPosition += new Vector3(dx, 0, 0);
            optionButtons.Add(button);

            ShopOptionButton option = button.GetComponent<ShopOptionButton>();
            (Upgrade upgrade, int price) = choices[i];
            Buff buff = upgrade.buff;
            option.buffText = buff.description;
            option.ownerText = upgrade.sourceName;
            option.price = price;
            option.currencyImage = DecideRubySprite(price);
            option.buyable = price <= budget;
            button.SetActive(true);

            int cur = i;
            option.OnClick += () =>
            {
                option.gameObject.SetActive(false);
                OnUpgradeChosen(upgrade, price, consumer);
            };
        }
    }

    public void ShowRefreshButton(int refreshPrice, ShopConsumer manager)
    {

    }

    public void OnFinish()
    {
        OnShoppingEnd?.Invoke();
        Destroy(gameObject);
    }

    private void OnUpgradeChosen(Upgrade upgrade, int price, ShopConsumer consumer)
    {
        consumer.OnUpgradeChosen(upgrade, price);
    }

    public void ClearButtons()
    {
        foreach (var button in optionButtons)
        {
            Destroy(button);
        }
    }

    private float GetOffsetX(int i, int n)
    {
        return ((float)i - (float)(n - 1) / 2f) * (buttonWidth + gapOfChoices);
    }

    private Sprite DecideRubySprite(int price)
    {
        for (int i = 0; i < rubyImageRange.Length; ++i)
        {
            if (price <= rubyImageRange[i]) return rubyImages[i];
        }
        return rubyImages[rubyImages.Length - 1];
    }
}
