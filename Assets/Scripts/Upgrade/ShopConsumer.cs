using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(WeaponHolder))]
[RequireComponent(typeof(ItemHolder))]
[RequireComponent(typeof(Currency))]
public class ShopConsumer : MonoBehaviour
{
    public int numberOfChoices { get; set; } = 3;
    public event Action OnShoppingEnd;

    public int basePrice = 10;
    public float weaponInflation = 1.5f;
    public float itemInflation = 1.2f;
    public float inflation = 1f;
    public float fluctuation = 1.3f;
    public int refreshPrice = 3;

    [SerializeField] private DefaultUpgraderHelper defaultUpgrades;
    [SerializeField] private GameObject menuPrefab;

    private Currency currency;
    private WeaponHolder weaponHolder;
    private ItemHolder itemHolder;

    private void Awake()
    {
        weaponHolder = GetComponent<WeaponHolder>();
        itemHolder = GetComponent<ItemHolder>();
        currency = GetComponent<Currency>();

        
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateEnter += (GameState prv, GameState cur) => {
            if (cur == GameState.Shopping)
            {
                GameManager.Instance.PauseTime();
                OpenShop();
            }
        };

        GameManager.Instance.OnGameStateExit += (GameState cur, GameState nxt) => {
            if (cur == GameState.Shopping)
            {
                GameManager.Instance.ResumeTime();
            }
        };

    }

    private void OpenShop()
    {
        var menuObj = Instantiate(menuPrefab);
        var menu = menuObj.GetComponent<ShopMenu>();
        menu.OnShoppingEnd += () => OnShoppingEnd?.Invoke();

        List<(Upgrade, int)> availibles = GeneratePool();
        var choicesNumber = GenerateChoices(availibles.Count, numberOfChoices);

        List<(Upgrade, int)> choices = new List<(Upgrade, int)>();
        foreach (var v in choicesNumber)
        {
            choices.Add(availibles[v]);
        }

        menu.ShowChoices(choices, this, currency.ruby);
        menu.ShowRefreshButton(refreshPrice, this);
    }

    public void OnUpgradeChosen(Upgrade upgrade, int price)
    {
        if (price > currency.ruby)
        {
            Debug.LogWarning("Not Enough Money! You are not supposed to be able to choose this one...");
            return;
        }

        currency.ruby -= price;

        upgrade.Activate(gameObject);
    }

    public List<int> GenerateChoices(int n, int k)
    {
        // choose `numberOfChoices` numbers from `availibles.Count` numbers randomly
        // https://stackoverflow.com/questions/26931528/random-number-generator-with-no-duplicates
        var rnd = new System.Random();
        var randomNumbers = Enumerable.Range(0, n).OrderBy(x => rnd.Next()).Take(k).ToList();

        return randomNumbers;
    }

    private List<(Upgrade, int)> GeneratePool()
    {
        List<(Upgrade, int)> availibles = new List<(Upgrade, int)>();

        availibles.AddRange(GenerateWeaponPoll());
        availibles.AddRange(GenerateItemPoll());
        // GenerateEvolutionPoll
        availibles.AddRange(GenerateDefaultPoll());
        return availibles;
    }

    private List<(Upgrade, int)> GenerateWeaponPoll()
    {
        List<(Upgrade, int)> availibles = new List<(Upgrade, int)>();
        if (weaponHolder == null) return availibles;
        foreach (var weapon in weaponHolder.WeaponList)
        {
            UpgraderHelper helper = weapon.GetComponent<UpgraderHelper>();
            if (helper == null) continue;
            foreach (var upgrade in helper.GetAvailableUpgrades())
            {
                availibles.Add((upgrade, randomWeaponPrice));
            }
        }
        return availibles;
    }

    private List<(Upgrade, int)> GenerateItemPoll()
    {
        List<(Upgrade, int)> availibles = new List<(Upgrade, int)>();
        if (itemHolder == null) return availibles;
        foreach (var item in itemHolder.itemList)
        {
            UpgraderHelper helper = item.GetComponent<UpgraderHelper>();
            if (helper == null) continue;
            foreach (var upgrade in helper.GetAvailableUpgrades())
            {
                availibles.Add((upgrade, randomItemPrice));
            }
        }
        return availibles;
    }

    private List<(Upgrade, int)> GenerateDefaultPoll()
    {
        List<(Upgrade, int)> availibles = new List<(Upgrade, int)>();
        foreach (Upgrade upgrade in defaultUpgrades.GetAvailableUpgrades())
        {
            availibles.Add((upgrade, randomDefaultPrice));
        }
        return availibles;
    }

    private int RandomInt(float min, float max)
    {
        return UnityEngine.Random.Range((int)Mathf.Ceil(min), (int)Mathf.Floor(max) + 1);
    }

    private int randomDefaultPrice
    {
        get
        {
            float min = basePrice * inflation / fluctuation;
            float max = basePrice * inflation * fluctuation;
            return RandomInt(min, max);
        }
    }

    private int randomWeaponPrice
    {
        get
        {
            float min = basePrice * inflation * weaponInflation / fluctuation;
            float max = basePrice * inflation * weaponInflation * fluctuation;
            return RandomInt(min, max);
        }
    }

    private int randomItemPrice
    {
        get
        {
            float min = basePrice * inflation * itemInflation / fluctuation;
            float max = basePrice * inflation * itemInflation * fluctuation;
            return RandomInt(min, max);
        }
    }
}
