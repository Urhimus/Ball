using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStateAnimation {
    Idle,
    Moving,
    Flying,
}

public class VisualPlayer : MonoBehaviour
{
    private MovingPlayer movingPlayer;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Animator animator;
    private float scaleStart;

    void Awake()
    {
        scaleStart = transform.localScale.y / Mathf.Abs(transform.localScale.y);
        movingPlayer = GetComponentInParent<MovingPlayer>();
        GameStateMachine.setGameState += SetState;
    }

    void SetState(GameState to, GameState from, StateArgs args)
    {
        if (to == GameState.BeforeStart)
            animator.SetInteger("State", (int)PlayerStateAnimation.Idle);
    }

    void Update()
    {
        Rotation();
        if (movingPlayer.moving)
        {
            if (GameStateMachine.instance.gameStateCurrent != GameState.BeforeStart)
                animator.SetInteger("State", (int)PlayerStateAnimation.Moving);

            if (movingPlayer.goingRightNow)
                transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y) * scaleStart);
            else
                transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y) * -scaleStart);
        }
        else
        {
            if (GameStateMachine.instance.gameStateCurrent != GameState.BeforeStart)
                animator.SetInteger("State", (int)PlayerStateAnimation.Flying);
        }
    }

    void Rotation()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            Quaternion.LookRotation(transform.forward, new Vector3(-movingPlayer.rb.velocity.y, movingPlayer.rb.velocity.x)), 
            rotationSpeed * Time.deltaTime);
    }
}
