using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stamina))]
public class StaminaDecreaser : IntAttribute
{
    private Stamina stamina;

    //Coroutine decrease;
    //bool isDecreasing = true;

    float time = 0f;

    private void Start()
    {
        stamina = GetComponent<Stamina>();
    }

    private void Update()
    {
        if (time >= 1f)
        {
            stamina.CurrentStamina -= value;
            time -= 1f;
        }

        time += Time.deltaTime;
    }

    /*private void OnEnable()
    {
        isDecreasing = true;
        decrease = StartCoroutine(Decrease());
    }

    private void OnDisable()
    {
        if (isDecreasing)
            StopCoroutine(decrease);
        isDecreasing = false;
    }

    private IEnumerator Decrease()
    {
        while (true)
        {
            stamina.CurrentStamina -= value;
            yield return new WaitForSeconds(1f);
        }
    }*/
}
