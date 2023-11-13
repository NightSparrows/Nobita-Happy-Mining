using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{

    private PlayerExperience expListen;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnDestroy()
    {
        expListen.OnMaxPlayerExpChanged -= UpdateMaxExp;
        expListen.OnPlayerExpChanged -= UpdateExp;
    }

    private void Start()
    {
        expListen = GameManager.Instance.player.GetComponent<PlayerExperience>();
        expListen.OnMaxPlayerExpChanged += UpdateMaxExp;
        expListen.OnPlayerExpChanged += UpdateExp;

        UpdateMaxExp(expListen.MaxPlayerExp);
        UpdateExp(expListen.CurrentPlayerExp);
    }

    private void UpdateMaxExp(int newMaxExp)
    {
        slider.maxValue = newMaxExp;
    }

    private void UpdateExp(int newExp)
    {
        slider.value = newExp;
    }
}
