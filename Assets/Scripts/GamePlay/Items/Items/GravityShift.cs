using System.Collections;
using UnityEngine;

public class GravityShift : UsableItem
{
    Vector2 gravityShift;
    [SerializeField] float gravityForce = 10;
    [SerializeField] float time;
    [SerializeField] bool push = true;
    [SerializeField] bool gravityMagnitudeIsNormal = true;
    Vector2 gravityStandart;

    protected override void Awake()
    {
        base.Awake();

        if (gravityMagnitudeIsNormal)
            gravityForce = Physics2D.gravity.magnitude;
        
        gravityStandart = Physics2D.gravity;
        gravityShift =  new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)) * gravityForce ;

        GameStateMachine.setGameState += delegate (GameState to, GameState from, StateArgs stateArgs)
        {
            if (to == GameState.BeforeStart)
                Physics2D.gravity = gravityStandart;
        };
    }

    public override void Action()
    {
        base.Action();
            if (time > 0)
                StartCoroutine(Length(time));
            else
            {
                Physics2D.gravity = gravityShift;
            }

        if (push)
        {
            movingPlayer.ChargeByVector(gravityShift, rbPlayer.velocity.magnitude);
        }
    }

    IEnumerator Length(float time)
    {
        Physics2D.gravity = gravityShift;
        yield return new WaitForSeconds(time);
        Physics2D.gravity = gravityStandart;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Physics2D.gravity = gravityStandart;
    }
}
