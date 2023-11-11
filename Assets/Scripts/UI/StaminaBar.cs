using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public GameObject player;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        slider.value -= 1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
