
public class StarDeath : Star
{
    bool diedOnLevel;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameStateMachine.setGameState += delegate (GameState to, GameState from, StateArgs stateArgs) {
            if (to == GameState.BeforeStart && (from == GameState.AfterStart || from == GameState.Pause) && !stateArgs.beginFromStart)
            {
                diedOnLevel = true;
            }
            else if (to == GameState.BeforeStart)
                diedOnLevel = false;
        };
    }

    public override bool ConditionMet()
    {
        return !diedOnLevel;
    }


    public override void Description()
    {
        tmPro.text = "Dont Die!";
    }
}
