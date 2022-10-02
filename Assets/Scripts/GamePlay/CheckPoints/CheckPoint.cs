using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{
    [HideInInspector]
    public bool checkPointIsActive;
    Vector3 scaleStandart;
    public bool goRight = true;
    public SwitchState switchState = SwitchState.First;
    private SpriteRenderer spriteRenderer;
    private float alpha;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = spriteRenderer.color.a;
        scaleStandart = transform.localScale;
        GameStateMachine.setGameState += SetDefaultStartPosition;
        GameStateMachine.setGameState += SetDefaultStartPosition;
    }

    public void OnPlayerEnter(GameObject startPoint)
    {
        if (!checkPointIsActive && GameStateMachine.instance.gameStateCurrent == GameState.AfterStart)
        {
            spriteRenderer.color = Color.white;
            startPoint.transform.position = transform.position;
            transform.localScale *= 1.2f;
            checkPointIsActive = true;
        }
    }

    void SetDefaultStartPosition(GameState to, GameState from, StateArgs args)
    {
        if (args.beginFromStart)
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            transform.localScale = scaleStandart;
            checkPointIsActive = false;
        }
    }
}
