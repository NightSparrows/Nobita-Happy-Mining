using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenuController : MonoBehaviour
{
    [SerializeField] private GameObject deadMenuUI;

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
        if (curState == GameState.Dead)
        {
            GameManager.Instance.PauseTime();
            deadMenuUI.SetActive(true);
        }
    }

    private void OnGameStateExit(GameState curState, GameState nextState)
    {
        if (curState == GameState.Dead)
        {
            GameManager.Instance.ResumeTime();
            deadMenuUI.SetActive(false);
        }
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.Restart();
    }
}
