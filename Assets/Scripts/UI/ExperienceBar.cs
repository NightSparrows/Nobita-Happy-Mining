using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{

    public GameObject player;
    private Slider slider;

    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void FixedUpdate()
    {
        counter += 1;
        if (counter == 10)
        {
            slider.value += 1;
            counter = 0;
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
