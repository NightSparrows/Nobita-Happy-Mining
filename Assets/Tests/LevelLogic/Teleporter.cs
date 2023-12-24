using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleporter : MonoBehaviour
{
    public KeyCode activatKey = KeyCode.F;
    public event Action OnTeleport;

    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(activatKey))
        {
            OnTeleport?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInRange = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInRange = false;
    }
}
