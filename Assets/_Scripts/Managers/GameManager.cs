using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    // Kick the game off with the first state
    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);
        
        Log($"New state: {newState}");

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.SpawnPrey:
                HandleSpawnPrey();
                break;
            case GameState.PlayerFreeRoam:
                HandlePlayerFreeRoam();
                break;
            case GameState.FightPrey:
                HandleFightPrey();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandleStarting() {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        
        ChangeState(GameState.SpawnPrey);
    }

    private void HandleSpawnPrey() {
        PreyManager.Instance.PopulateArea();
        
        ChangeState(GameState.PlayerFreeRoam);
    }

    private void HandlePlayerFreeRoam() {
        
        // Spawn enemies
        
        ChangeState(GameState.FightPrey);
    }

    private void HandleFightPrey() {
        // If you're making a turn based game, this could show the turn menu, highlight available units etc
        
        // Keep track of how many units need to make a move, once they've all finished, change the state. This could
        // be monitored in the unit manager or the units themselves.
    }
}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState {
    Starting,
    SpawnPrey,
    PlayerFreeRoam,
    FightPrey,
    Win,
    Lose,
}