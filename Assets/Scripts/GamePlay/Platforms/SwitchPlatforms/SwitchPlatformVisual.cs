using UnityEngine;
using UnityEngine.U2D;

public class SwitchPlatformVisual : MonoBehaviour
{
    [SerializeField] private SpriteShapeController[] spriteShapeControllers;
    [SerializeField] private SwitchState[] switchStates;
    private int counter = 0;

    private void Start()
    {
        var temp = GetComponentsInChildren<SwitchPlatform>();
        spriteShapeControllers = new SpriteShapeController[temp.Length];
        switchStates = new SwitchState[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            switchStates[i] = temp[i].GetComponent<SwitchPlatform>().switchState;
            spriteShapeControllers[i] = temp[i].GetComponent<SpriteShapeController>();
        }

        Switcher.changeSwitchState += Switch;
    }

    private void Switch(SwitchState switchState)
    {
        for (int i = 0; i < switchStates.Length; i++)
        {
            for (; counter < spriteShapeControllers[i].spline.GetPointCount() - 1; counter++)
                spriteShapeControllers[i].spline.SetSpriteIndex(counter, (int)switchState);
            counter = 0;
        }
    }
}
