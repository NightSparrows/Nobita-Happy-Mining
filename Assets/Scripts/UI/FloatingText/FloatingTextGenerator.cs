using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pfPopup;

    public void Generate(Vector3 position, int value, float lifetime = 1f)
    {
        GameObject popup = Instantiate(pfPopup, position, Quaternion.identity);
        TextMeshProUGUI text = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = value.ToString();
        Destroy(popup, lifetime);
    }
}
