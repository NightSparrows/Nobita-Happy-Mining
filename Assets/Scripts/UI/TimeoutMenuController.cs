using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeoutMenuController : MonoBehaviour
{
    [SerializeField] private GameObject timeoutMenuUI;

    private void Start()
    {
        GameManager.Instance.OnGameStateEnter += OnGameStateEnter;
        GameManager.Instance.OnGameStateExit += OnGameStateExit;
    }

    private void OnDestroy()
    {
        if (!GameManager.DoesInstanceExit()) return;
        GameManager.Instance.OnGameStateEnter -= OnGameStateEnter;
        GameManager.Instance.OnGameStateExit -= OnGameStateExit;
    }

    private void OnGameStateEnter(GameState prvState, GameState curState)
    {
        if (curState == GameState.Timeout)
        {
            GameManager.Instance.PauseTime();
            timeoutMenuUI.SetActive(true);
        }
    }

    private void OnGameStateExit(GameState curState, GameState nextState)
    {
        if (curState == GameState.Timeout)
        {
            GameManager.Instance.ResumeTime();
            timeoutMenuUI.SetActive(false);
        }
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.Restart();
    }
}
