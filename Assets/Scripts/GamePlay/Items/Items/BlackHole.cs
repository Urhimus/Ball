using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : UsableItem
{
    [SerializeField] private float gravityForce = 10;
    [SerializeField] private Rigidbody2D playerRB;
    private float gravityStandart;
    
    override protected void Awake()
    {
        base.Awake();
        playerRB = player.GetComponent<Rigidbody2D>();
        gravityStandart = playerRB.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            playerRB.gravityScale = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            playerRB.gravityScale = 0;
            playerRB.AddForce(gravityForce * (transform.position - playerRB.transform.position).normalized);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            playerRB.gravityScale = gravityStandart;
    }
}
