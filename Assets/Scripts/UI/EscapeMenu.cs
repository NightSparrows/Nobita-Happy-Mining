using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.Restart();
    }
}
