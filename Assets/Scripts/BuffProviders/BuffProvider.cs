using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffProvider : MonoBehaviour
{
    public bool IsInvalid
    {
        get
        {
            return gameObject == null;
        }
    }

    public float Duration
    {
        set
        {
            Destroy(gameObject, value);
        }
    }
}
