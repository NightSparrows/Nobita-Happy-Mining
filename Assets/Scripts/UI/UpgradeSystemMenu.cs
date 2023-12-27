using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystemMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionButtomPrefab;
    [SerializeField] private Transform optionContainer;

    [SerializeField] private float gapOfChoices = 100f;

    private float buttonWidth;

    private List<GameObject> optionButtons;

    private void Awake()
    {
        buttonWidth = optionButtomPrefab.GetComponent<RectTransform>().sizeDelta.x;
    }

    // called by UpgradeManager, where the choices are given
    public void ShowChoices(List<Upgrade> choices, UpgradeManager manager)
    {
        //Debug.Log("Choice count= " + choices.Count);
        optionButtons = new List<GameObject>();

        int n = choices.Count;

        for (int i = 0; i < n; ++i)
        {
            GameObject button = Instantiate(optionButtomPrefab, optionContainer);
            float dx = GetOffsetX(i, n);
            button.transform.localPosition += new Vector3(dx, 0, 0);
            optionButtons.Add(button);

            OptionButton option = button.GetComponent<OptionButton>();
            Buff buff = choices[i].buff;
            option.buffText = buff.description;
            option.ownerText = choices[i].sourceName;
            
            int cur = i;
            option.OnClick += () => manager.OnUpgradeChosen(choices[cur]);
        }
    }

    public void ClearButton()
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
