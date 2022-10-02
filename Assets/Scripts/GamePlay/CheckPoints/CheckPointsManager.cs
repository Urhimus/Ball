using UnityEngine;
using System;

public class CheckPointsManager : MonoBehaviour
{

    private Vector3 startPointStartPos;
    private GameObject startPoint;

    public static Action<CheckPoint> checkPointActivated;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        startPoint = GameObject.Find("StartPoint");
        startPointStartPos = startPoint.transform.position;

        GameStateMachine.setGameState += RestartTheLevel;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checkPoint")
        {
            CheckPoint checkPoint = collision.gameObject.GetComponent<CheckPoint>();
            if (!checkPoint.checkPointIsActive)
            {
                audioSource.Play();
                GameStateMachine.instance.stateArgs.beginFromStart = false;
                checkPoint.OnPlayerEnter(startPoint);
                checkPointActivated?.Invoke(checkPoint);
            }
        }
    }
    public void RestartTheLevel(GameState to, GameState from, StateArgs stateArgs)
    {
        if (stateArgs.beginFromStart)
            startPoint.transform.position = startPointStartPos;
    }

    private void OnDisable()
    {
        checkPointActivated = null;
    }
}
