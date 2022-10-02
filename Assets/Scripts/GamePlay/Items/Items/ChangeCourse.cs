using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCourse : UsableItem
{
    override protected void Awake()
    {
        base.Awake();
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    public override void Action()
    {
        base.Action();
        if (playerIsOnItem && !onCooldown)
        {
            rbPlayer.velocity *= -1;
            StartCoroutine(Cooldown());
        }
    }
}
