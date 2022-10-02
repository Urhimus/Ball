using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        animator.SetTrigger("LoadEnd");
        GameStateMachine.setGameState += delegate (GameState to, GameState from, StateArgs args)
        {
            if (to == GameState.BeforeStart && from == GameState.AfterStart)
                animator.SetTrigger("ReloadBegin");
        };
        SceneManagerScript.sceneAsyncLoadingBegan += delegate { animator.SetTrigger("LoadBegin"); };
    }

}
