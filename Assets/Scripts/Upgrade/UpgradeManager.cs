using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(WeaponHolder))]
[RequireComponent(typeof(ItemHolder))]
public class UpgradeManager : MonoBehaviour
{
    public int numberOfChoices { get; set; } = 3;
    public event Action OnUpgradeEnd; 

    [SerializeField] private DefaultUpgraderHelper defaultUpgrades;
    [SerializeField] private GameObject menuPrefab;

    private WeaponHolder weaponHolder;
    private ItemHolder itemHolder;

    private void Awake()
    {
        weaponHolder = GetComponent<WeaponHolder>();
        itemHolder = GetComponent<ItemHolder>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateEnter += (GameState prv, GameState cur) => {
            if (cur == GameState.Upgrading)
            {
                GameManager.Instance.PauseTime();
                Upgrade();
            }
        };

        GameManager.Instance.OnGameStateExit += (GameState cur, GameState nxt) => {
            if (cur == GameState.Upgrading)
            {
                GameManager.Instance.ResumeTime();
            }
        };
    }

    // Pause the game, Generate upgrade choices and Show the choices
    // Intended to be Called when Player Level up
    private void Upgrade()
    {
        var menuObj = Instantiate(menuPrefab);
        var menu = menuObj.GetComponent<UpgradeSystemMenu>();

        List<Upgrade> availibles = GeneratePool();
        var choicesNumber = GenerateChoices(availibles.Count, numberOfChoices);

        List<Upgrade> choices = new List<Upgrade>();
        foreach (var v in choicesNumber)
        {
            choices.Add(availibles[v]);
        }

        menu.ShowChoices(choices, this);
    }

    public void OnUpgradeChosen(Upgrade upgrade)
    {        
        upgrade.Activate(gameObject);
        OnUpgradeEnd?.Invoke();
    }

    public List<int> GenerateChoices(int n, int k)
    {
        // choose `numberOfChoices` numbers from `availibles.Count` numbers randomly
        // https://stackoverflow.com/questions/26931528/random-number-generator-with-no-duplicates
        var rnd = new System.Random();
        var randomNumbers = Enumerable.Range(0, n).OrderBy(x => rnd.Next()).Take(k).ToList();

        return randomNumbers;
    }

    private List<Upgrade> GeneratePool()
    {
        List<Upgrade> availibles = new List<Upgrade>();

        availibles.AddRange(GenerateWeaponPoll());
        availibles.AddRange(GenerateItemPoll());
        // GenerateEvolutionPoll
        availibles.AddRange(GenerateDefaultPoll());
        return availibles;
    }

    private List<Upgrade> GenerateWeaponPoll()
    {
        List<Upgrade> availibles = new List<Upgrade>();
        if (weaponHolder == null) return availibles;
        foreach (var weapon in weaponHolder.WeaponList)
        {
            UpgraderHelper helper = weapon.GetComponent<UpgraderHelper>();
            if (helper == null) continue;
            availibles.AddRange(helper.GetAvailableUpgrades());
        }
        return availibles;
    }

    private List<Upgrade> GenerateItemPoll()
    {
        List<Upgrade> availibles = new List<Upgrade>();
        if (itemHolder == null) return availibles;
        foreach (var item in itemHolder.itemList)
        {
            UpgraderHelper helper = item.GetComponent<UpgraderHelper>();
            if (helper == null) continue;
            availibles.AddRange(helper.GetAvailableUpgrades());
        }
        return availibles;
    }

    private List<Upgrade> GenerateDefaultPoll()
    {
        List<Upgrade> availibles = new List<Upgrade>();
        foreach (Upgrade upgrade in defaultUpgrades.GetAvailableUpgrades())
        {
            availibles.Add(upgrade);
        }
        return availibles;
    }
}
