using System.Collections;
using UnityEngine;
using System;

public abstract class UsableItem : MonoBehaviour
{
    [SerializeField] ActionItemType itemType = ActionItemType.PressItem;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public MovingPlayer movingPlayer;
    [HideInInspector]
    public Rigidbody2D rbPlayer;

    [HideInInspector]
    public bool autoUse = false;

    public float cooldown = 1;
    protected bool onCooldown = false;
    public event Action<bool> onCooldownEvent;

    protected bool playerIsOnItem = false;
    public event Action<bool> onPlayerEvent;

    AudioSource audioSource;

    [SerializeField] private bool isHided = false;

    protected virtual void Awake()
    {
        if (!isHided)
            audioSource = GetComponent<AudioSource>();

        switch (itemType)
        {
            case ActionItemType.PressItem:
                autoUse = false;
                tag = "pressItem";
                break;
            case ActionItemType.PickItem:
                autoUse = true;
                tag = "pickItem";
                break;
        }
        player = GameObject.Find("Player");
        movingPlayer = player.GetComponent<MovingPlayer>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        GameStateMachine.setGameState += delegate (GameState to, GameState from, StateArgs args)
        {
            if (to == GameState.BeforeStart)
            {
                onCooldown = false;
                onCooldownEvent?.Invoke(onCooldown);
            }
        };
    }

    public virtual void Action() {
        if (!isHided && !onCooldown && audioSource != null)
            audioSource.Play();
    }

    public virtual IEnumerator Cooldown()
    {
        if (cooldown > 0)
        {
            onCooldown = true;
            onCooldownEvent?.Invoke(onCooldown);
            yield return new WaitForSeconds(cooldown);
            onCooldown = false;
            onCooldownEvent?.Invoke(onCooldown);
        }
    }

    public void OnPlayer(bool onPlayer)
    {
        playerIsOnItem = onPlayer;
        onPlayerEvent?.Invoke(playerIsOnItem);
    }

    protected virtual void OnDisable()
    {
        onCooldownEvent = null;
        onPlayerEvent = null;
    }
}

public enum ActionItemType
{
    PressItem,
    AreaItem,
    PickItem
}
