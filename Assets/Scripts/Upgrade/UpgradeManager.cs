using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerExperience))]
[RequireComponent(typeof(WeaponHolder))]
public class UpgradeManager : MonoBehaviour
{
    public int numberOfChoices { get; set; } = 3;

    [SerializeField] private Buff[] guaranteedBuffs;
    [SerializeField] private UpgradeSystemMenu menu;

    private WeaponHolder weaponHolder;
    

    private List<(GameObject, Buff, object)> availibles;

    private void Upgrade()
    {
        GeneratePool();
        var choicesNumber = GenerateChoices();
        List<(GameObject, Buff, object)> choices = new List<(GameObject, Buff, object)>();
        foreach (var v in choicesNumber)
        {
            choices.Add(availibles[v]);
        }
        menu.gameObject.SetActive(true);
        menu.ShowChoices(choices);
    }

    private void Awake()
    {
        PlayerExperience exp = GetComponent<PlayerExperience>();
        exp.OnPlayerLevelChanged += x => Upgrade();

        weaponHolder = GetComponent<WeaponHolder>();
        if (menu == null)
        {
            Debug.LogError("UpgradeSystemMenu not found for UpgradeManager");
        }
    }

    public void ChooseBuff(GameObject owner, Buff buff, object indicator)
    {
        if (owner != null)
        {
            owner.GetComponent<UpgraderHelper>().Upgrade(indicator, gameObject);
        }
        else
        {
            buff.ApplyTo(gameObject);
        }
        menu.gameObject.SetActive(false);
    }

    public List<int> GenerateChoices()
    {
        // https://stackoverflow.com/questions/26931528/random-number-generator-with-no-duplicates
        var rnd = new System.Random();
        var randomNumbers = Enumerable.Range(0, availibles.Count).OrderBy(x => rnd.Next()).Take(numberOfChoices).ToList();

        return randomNumbers;
    }

    private void GeneratePool()
    {
        availibles = new List<(GameObject, Buff, object)>();

        GenerateWeaponPoll();
        // GenerateItemPoll
        // GenerateEvolutionPoll
        GenerateGuaranteePoll();
    }

    private void GenerateWeaponPoll()
    {
        foreach (var weapon in weaponHolder.WeaponList)
        {
            UpgraderHelper helper = weapon.GetComponent<UpgraderHelper>();
            if (helper == null) continue;
            foreach (var (buff, indicator) in helper.GetAvailableUpgrades())
            {
                availibles.Add((weapon, buff, indicator));
            }
        }
    }

    private void GenerateGuaranteePoll()
    {
        for (int i = 0; i < guaranteedBuffs.Length && availibles.Count < numberOfChoices; ++i)
        {
            availibles.Add((null, guaranteedBuffs[i], null));
        }
    }
}
