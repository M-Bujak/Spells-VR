using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InMainMenu,
    Playing,
    Won,
    Lost
}

public class GameStateManager : MonoBehaviour
{
    [field: SerializeField]
    public TowerDestroyableBehaviour Tower { get; set; }

    public static GameStateManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public Action OnInMainMenu { get; set; }
    public Action OnPlaying { get; set; }
    public Action OnGameLost { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeCurrentState(GameState.InMainMenu);
        TowerDestroyableBehaviour.OnTowerDemolished += LoseGame;
    }

    private void LoseGame()
    {
        ChangeCurrentState(GameState.Lost);
    }

    private void ChangeCurrentState(GameState newState)
    {
        switch(newState)
        {
            case GameState.InMainMenu:
                OnInMainMenu?.Invoke();
                break;
            case GameState.Playing:
                OnPlaying?.Invoke();
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                OnGameLost?.Invoke();
                break;
        }

        CurrentState = newState;
    }

    public void StartGame()
    {
        if(CurrentState == GameState.InMainMenu)
        {
            ChangeCurrentState(GameState.Playing);
        }
    }

    public void GoToMainMenu()
    {
        if (CurrentState == GameState.Lost)
        {
            ChangeCurrentState(GameState.InMainMenu);
        }
    }

    private void OnDestroy()
    {
        TowerDestroyableBehaviour.OnTowerDemolished -= LoseGame;
    }
}
