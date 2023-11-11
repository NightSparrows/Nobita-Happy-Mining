using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Healthable
{

    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) health -= 20;
    }
}
