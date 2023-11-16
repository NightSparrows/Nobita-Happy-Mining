using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private PlayerExperience _listen; // set to Player in default
    [SerializeField] private ValueBar _valueBar;

    private void Start()
    {
        if (_listen == null)
        {
            if (GameManager.Instance.player != null)
                _listen = GameManager.Instance.player.GetComponent<PlayerExperience>();
            else
                Debug.LogError("The target of Health Bar is missing!");
        }
        _listen.OnMaxPlayerExpChanged += UpdateMaxValue;
        _listen.OnPlayerExpChanged += UpdateValue;

        UpdateMaxValue(_listen.MaxPlayerExp);
        UpdateValue(_listen.CurrentPlayerExp);
    }

    private void OnDestroy()
    {
        _listen.OnMaxPlayerExpChanged -= UpdateMaxValue;
        _listen.OnPlayerExpChanged -= UpdateValue;
    }

    private void UpdateMaxValue(int newMaxValue)
    {
        _valueBar.MaxValue = newMaxValue;
    }

    private void UpdateValue(int newValue)
    {
        _valueBar.Value = newValue;
    }
}
