using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void Start()
    {
        GameManager.Instance.OnGameStateEnter += OnGameStateEnter;
        GameManager.Instance.OnGameStateExit += OnGameStateExit;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateEnter -= OnGameStateEnter;
        GameManager.Instance.OnGameStateExit -= OnGameStateExit;
    }

    private void OnGameStateEnter(GameState prvState, GameState curState)
    {
        if (curState == GameState.Victory)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    private void OnGameStateExit(GameState curState, GameState nextState)
    {
        if (curState == GameState.Victory)
        {
            pauseMenuUI.SetActive(false);
        }
    }
}
