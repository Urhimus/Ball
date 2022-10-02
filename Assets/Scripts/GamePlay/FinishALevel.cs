using UnityEngine;

public class FinishALevel : MonoBehaviour
{
    [SerializeField] private AudioSource finishSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && GameStateMachine.instance.gameStateCurrent != GameState.Finish)
        {
            finishSound.Play();
            DataPersistentManager.instance.SaveGame();
            GameStateMachine.instance.SetState(3, new StateArgs(true));
        }
    }
}