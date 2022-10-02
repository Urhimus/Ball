using UnityEngine;
using System;

public enum GameState
{
    BeforeStart = 0,
    AfterStart = 1,
    Pause = 2,
    Finish = 3,
}

public class GameStateMachine : MonoBehaviour
{
    public ButtonState[] buttons;
    public GameState gameStateCurrent;
    public static event Action<GameState, GameState, StateArgs> setGameState;
    public bool firstCheckpointEntered;
    public StateArgs stateArgs;
    public bool menu = false;

    public static GameStateMachine instance;

    private void Start()
    {
        stateArgs = new StateArgs();
        instance = this;
        if (!menu)
            SetState(0, new StateArgs(true));
        else
            SetState(1, new StateArgs(true));
    }

    public void SetState(int gameStateInt, StateArgs stateArgs)
    {
        this.stateArgs = stateArgs;

        setGameState?.Invoke((GameState)gameStateInt, gameStateCurrent, stateArgs);

        switch ((GameState)gameStateInt)
        {
            case GameState.BeforeStart:
                Time.timeScale = 1;
                break;
            case GameState.AfterStart:
                Time.timeScale = 1;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Finish:
                break;
        }
        gameStateCurrent = (GameState)gameStateInt;

        ActivateUI();

        void ActivateUI()
        {
            foreach (ButtonState button in buttons)
            {
                foreach (GameState buttonGameState in button.buttonGameState)
                {
                    if (buttonGameState == gameStateCurrent)
                    {
                        button.button.gameObject.SetActive(true);
                        break;
                    }
                    button.button.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetState(int gameStateInt)
    {
        SetState(gameStateInt, stateArgs);
    }

    public void SetRestart()
    {
        SetState(0, new StateArgs());
    }

    private void OnDisable()
    {
        setGameState = null;
        instance = null;
    }
}
public class StateArgs
{
    public bool beginFromStart;

    public StateArgs(bool start)
    {
        this.beginFromStart = start;
    }

    public StateArgs()
    {
        beginFromStart = true;
    }
}
