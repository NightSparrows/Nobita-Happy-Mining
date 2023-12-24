using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stamina))]
public class StaminaDecreaser : IntAttribute
{
    private Stamina stamina;

    private void Start()
    {
        stamina = GetComponent<Stamina>();
        StartCoroutine(Decrease());
    }

    private IEnumerator Decrease()
    {
        while (true)
        {
            stamina.CurrentStamina -= value;
            yield return new WaitForSeconds(1f);
        }
    }
}
