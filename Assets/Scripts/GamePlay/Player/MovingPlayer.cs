using System.Collections;
using UnityEngine;
using System;

public class MovingPlayer : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] float speedPlayer;
    [SerializeField] private float leprSpeed = 1;
    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public Vector2 addSpeedCurrent;
    [HideInInspector]
    public bool goingRightNow;

    private float speedStandartCurrent;
    private float speedStandartAtStart;
    private float speedStandartAddOnCheckPoint;

    [SerializeField] bool goRight = true;
    bool goRightAtStart;

    private float gravityScaleAtStart;

    Vector2 gravityAtStart;
    Vector2 gravityCurrent;

    private GameObject startPoint;

    private void Start()
    {
        startPoint = GameObject.Find("StartPoint");

        gravityScaleAtStart = rb.gravityScale;
        goRightAtStart = goRight;
        speedStandartCurrent = speedPlayer;
        speedStandartAtStart = speedStandartCurrent;
        gravityAtStart = Physics2D.gravity;
        gravityCurrent = gravityAtStart;

        CheckPointsManager.checkPointActivated += CheckPointActivated;
        GameStateMachine.setGameState += SetState;
    }

    void SetState(GameState to, GameState from, StateArgs args)
    {
        if (args.beginFromStart)
        {
            goRight = goRightAtStart;
            gravityCurrent = gravityAtStart;
            speedStandartCurrent = speedStandartAtStart;
        }

        if (to == GameState.BeforeStart)
        {
            moving = false;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.gravityScale = gravityScaleAtStart;
            speedPlayer = speedStandartCurrent;
            Physics2D.gravity = gravityCurrent;
            transform.position = startPoint.transform.position;
            rb.bodyType = RigidbodyType2D.Static;
            StopAllCoroutines();
        }
        else
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void CheckPointActivated(CheckPoint checkPoint)
    {
        gravityCurrent = Physics2D.gravity;
        goRight = checkPoint.goRight;
        speedStandartCurrent += speedStandartAddOnCheckPoint;
        speedStandartAddOnCheckPoint = 0;
    }

    void MoveModeVector()
    {
            if (goingRightNow)
            {
                if (rb.velocity.magnitude < speedPlayer)
                rb.velocity = Vector2.Lerp(rb.velocity, addSpeedCurrent * speedPlayer, leprSpeed * Time.deltaTime);
            }
            else
                if (rb.velocity.magnitude < speedPlayer)
                rb.velocity = Vector2.Lerp(rb.velocity, -addSpeedCurrent * speedPlayer, leprSpeed * Time.deltaTime);
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        moving = true;
        addSpeedCurrent = new Vector2(collision.contacts[0].normal.y, -collision.contacts[0].normal.x).normalized;
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            goingRightNow = ((Vector2.Dot(rb.velocity, addSpeedCurrent) >= -1 && goRight) || (Vector2.Dot(rb.velocity, addSpeedCurrent) >= 1 && !goRight));
            MoveModeVector();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        moving = false;
    }

    public void SpeedUp(float speed, float time, bool cancel)
    {
        if (cancel)
        {
            speedPlayer = speedStandartCurrent;
            StopAllCoroutines();
        }
        if (time == 0)
        {
            speedPlayer += speed;
            speedStandartAddOnCheckPoint += speed;
        }
        else
            StartCoroutine(SpeedBoost(speed, time));

        
    }

    IEnumerator SpeedBoost(float speed, float time)
    {
        speedPlayer += speed;
        yield return new WaitForSeconds(time);
        speedPlayer -= speed;
    }

    public void ChargeByVector(Vector2 chargeVector, float jumpSpeed)
    {
        rb.velocity = chargeVector.normalized * jumpSpeed;
    }
}
