using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class UsableItemVisualShape : MonoBehaviour
{
    [SerializeField] private Sprite spriteActivable, spriteAutoUse;

    private SpriteShapeController thisShapeController;

    private UsableItem item;

    private Color colorBase;
    private Color colorChangeToOnCooldown = Color.red;
    private float alpha = 0.35f;

    public Image buttonImage;
    virtual protected void Awake()
    {
        thisShapeController = GetComponent<SpriteShapeController>();

        item = GetComponent<UsableItem>();
        if (!item.autoUse)
        {
            colorBase = thisShapeController.spriteShapeRenderer.color;
            colorChangeToOnCooldown = Color.red;
            alpha = 0.35f;
        } else
        {
            thisShapeController.spriteShapeRenderer.color = new Color(0, 1, 0.5838819f, 1);
            colorBase = thisShapeController.spriteShapeRenderer.color;
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
            thisShapeController.spriteShapeRenderer.color = colorChangeToOnCooldown;
        }
        else
        {
            thisShapeController.spriteShapeRenderer.color = colorBase;
        }
    }

    virtual protected void OnPlayer(bool onPlayer)
    {
        if (onPlayer)
        {
            thisShapeController.spriteShapeRenderer.color = new Color(thisShapeController.spriteShapeRenderer.color.r, thisShapeController.spriteShapeRenderer.color.g, thisShapeController.spriteShapeRenderer.color.b, 1);
        }
        else
        {
            thisShapeController.spriteShapeRenderer.color = new Color(thisShapeController.spriteShapeRenderer.color.r, thisShapeController.spriteShapeRenderer.color.g, thisShapeController.spriteShapeRenderer.color.b, alpha);
        }
    }
}
