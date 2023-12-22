using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : FloatAttribute
{
    void Start()
    {
        Destroy(gameObject, value);
    }

}
