using UnityEngine;

public class SpeedBooster : UsableItem
{
    [SerializeField] private float speedAdd;
    [SerializeField] private float time;
    [SerializeField] private bool cancel = false;


    protected override void Awake()
    {
        base.Awake();
        movingPlayer = player.GetComponent<MovingPlayer>();
    }

    public override void Action()
    {
        base.Action();
        if (playerIsOnItem && !onCooldown)
        {
            movingPlayer.SpeedUp(speedAdd, time, cancel);
            StartCoroutine(Cooldown());
        }
    }
}
