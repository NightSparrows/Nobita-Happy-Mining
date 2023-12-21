using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void OnResumeButtonPressed()
    {
        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
    }

    public void OnMenuButtonPressed()
    {
        //controller.Back2Menu();
        GameManager.Instance.ResumeTime();
        gameObject.SetActive(false);
        GameManager.Instance.Restart();
    }

    public void OnOtherPressed()
    {
        //controller.OtherThings();
    }
}
