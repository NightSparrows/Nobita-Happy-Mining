using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private PlayerExperience _listen; // set to Player in default
    [SerializeField] private ValueBar _valueBar;

    private TextMeshProUGUI text;

    private void Start()
    {
        if (_listen == null)
        {
            if (GameManager.Instance.player != null)
                _listen = GameManager.Instance.player.GetComponent<PlayerExperience>();
            else
                Debug.LogError("The target of Health Bar is missing!");
        }

        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

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
        text.text = string.Format("Lv.{0} {1}", _listen.PlayerLevel, text.text);
    }

    private void UpdateValue(int newValue)
    {
        _valueBar.Value = newValue;
        text.text = string.Format("Lv.{0} {1}", _listen.PlayerLevel, text.text);
    }
}
