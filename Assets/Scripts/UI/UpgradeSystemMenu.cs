using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystemMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionButtomPrefab;
    [SerializeField] private Transform optionContainer;
    [SerializeField] private UpgradeManager manager;

    [SerializeField] private float gapOfChoices = 100f;

    private float buttonWidth;

    private List<(GameObject, Buff, object)> choices;
    private List<GameObject> optionButtons;

    private void Awake()
    {
        buttonWidth = optionButtomPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void ShowChoices(List<(GameObject, Buff, object)> choices)
    {
        optionButtons = new List<GameObject>();
        this.choices = choices;

        int n = choices.Count;

        for (int i = 0; i < n; ++i)
        {
            GameObject button = Instantiate(optionButtomPrefab, optionContainer);
            float dx = GetOffsetX(i, n);
            button.transform.localPosition += new Vector3(dx, 0, 0);
            optionButtons.Add(button);

            OptionButton option = button.GetComponent<OptionButton>();
            var (source, buff, _) = choices[i];
            option.buffText = buff.description;
            option.ownerText = (source != null)? source.name : "";

            int cur = i;
            option.OnClick += () => Choose(cur);
        }
    }

    public void Choose(int index)
    {
        ClearButton();
        var (source, buff, indicator) = choices[index];
        manager.ChooseBuff(source, buff, indicator);
    }

    private void ClearButton()
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
}
