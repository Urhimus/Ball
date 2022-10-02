using UnityEngine;

public class Jumper : UsableItem
{
    [SerializeField] private bool changeDir = false;
    [SerializeField] private float jumpSpeed = 10;
    private Vector2 rot;

    override protected void Awake()
    {
        base.Awake();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        rot = new Vector2( Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
    }

    override public void Action()
    {
        base.Action();
            if (playerIsOnItem && !onCooldown)
            {
                if (!changeDir)
                    rbPlayer.AddForce((rot * jumpSpeed), ForceMode2D.Impulse);
                else
                    movingPlayer.ChargeByVector(rot, jumpSpeed);
                StartCoroutine(Cooldown());
            }
    }
}
