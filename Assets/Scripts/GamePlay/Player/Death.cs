using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Death : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Die()
    {
        GameStateMachine.instance.SetState(0, GameStateMachine.instance.stateArgs);
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "death" && GameStateMachine.instance.gameStateCurrent != GameState.Finish)
            Die();
    }
}
