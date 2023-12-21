using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenuUI;

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
        if (curState == GameState.Victory)
        {
            GameManager.Instance.PauseTime();
            escapeMenuUI.SetActive(true);
        }
    }

    private void OnGameStateExit(GameState curState, GameState nextState)
    {
        if (curState == GameState.Victory)
        {
            GameManager.Instance.ResumeTime();
            escapeMenuUI.SetActive(false);
        }
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.Restart();
    }
}
