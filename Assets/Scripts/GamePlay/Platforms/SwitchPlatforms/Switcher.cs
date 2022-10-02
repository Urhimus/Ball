using UnityEngine;
using System;

public class Switcher : MonoBehaviour
{
    public static SwitchState currentSwitch = SwitchState.First;
    public static SwitchState startSwitch = SwitchState.First;
    [SerializeField]
    public static event Action<SwitchState> changeSwitchState;

    private void Awake()
    {
        startSwitch = SwitchState.First;
        GameStateMachine.setGameState += SetBeforeStartState;
        currentSwitch = SwitchState.First;
        CheckPointsManager.checkPointActivated += delegate (CheckPoint checkPoint) { startSwitch = checkPoint.switchState; };
        GameStateMachine.setGameState += delegate (GameState to, GameState from, StateArgs args) { if (to == GameState.Finish) startSwitch = SwitchState.First; };
        changeSwitchState?.Invoke(currentSwitch);
    }

    public void Switching()
    {
        if (currentSwitch != SwitchState.Second)
            currentSwitch++;
        else
            currentSwitch = SwitchState.First;

        changeSwitchState?.Invoke(currentSwitch);
    }

    void SetBeforeStartState(GameState to, GameState from, StateArgs stateArgs)
    {
        if (stateArgs.beginFromStart)
            startSwitch = SwitchState.First;
        if (to == GameState.BeforeStart)
        {
            currentSwitch = startSwitch;
            changeSwitchState?.Invoke(currentSwitch);
        }
    }

    private void OnDisable()
    {
        changeSwitchState = null;
    }
}

public enum SwitchState
{
    First, Second
}
