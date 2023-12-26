using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public KeyCode pauseKey = KeyCode.Escape;

    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (GameManager.Instance.isPaused)
            {
                GameManager.Instance.ResumeTime();
                pauseMenuUI.SetActive(false);
            }
            else
            {
                GameManager.Instance.PauseTime();
                pauseMenuUI.SetActive(true);
            }
        }

    }

    public void OnResumeButtonPressed()
    {
        GameManager.Instance.ResumeTime();
        pauseMenuUI.SetActive(false);
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.Restart();
    }

    public void OnOtherButtonPressed()
    {
        Debug.Log("other!");
    }
}
