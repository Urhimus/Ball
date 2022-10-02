using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UsableItemVisual : MonoBehaviour
{
    [SerializeField] private Sprite spriteActivable, spriteAutoUse;
    [SerializeField] private Animator animator;
    private Color colorBase;
    private Color colorChangeToOnCooldown = Color.red;
    private float alpha = 0.35f;

    private UsableItem usableItem;


    private SpriteRenderer thisSpriteRenderer;
    public Image buttonImage;
    virtual protected void Awake()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        usableItem = GetComponent<UsableItem>();

        if (!usableItem.autoUse)
        {
            colorBase = thisSpriteRenderer.color;
            colorChangeToOnCooldown = Color.red;
            alpha = 0.35f;
        }
        else
        {
            thisSpriteRenderer.color = new Color(0, 1, 0.5838819f);
            colorBase = thisSpriteRenderer.color;
            colorChangeToOnCooldown = Color.red;
            alpha = 1;
        }

        buttonImage = GameObject.Find("PressButton").GetComponent<Image>();


        GetComponent<UsableItem>().onPlayerEvent += OnPlayer;
        GetComponent<UsableItem>().onCooldownEvent += OnCooldown;
    }

    virtual protected void OnCooldown(bool onCooldown)
    {
        if (onCooldown)
        {
            thisSpriteRenderer.color = colorChangeToOnCooldown;
        }
        else
        {
            thisSpriteRenderer.color = colorBase;
        }
    }

    virtual protected void OnPlayer(bool onPlayer)
    {
        if (onPlayer)
        {
            thisSpriteRenderer.color = new Color(thisSpriteRenderer.color.r, thisSpriteRenderer.color.g, thisSpriteRenderer.color.b, 1);
        }
        else
        {
            thisSpriteRenderer.color = new Color(thisSpriteRenderer.color.r, thisSpriteRenderer.color.g, thisSpriteRenderer.color.b, alpha);
        }
    }
}
