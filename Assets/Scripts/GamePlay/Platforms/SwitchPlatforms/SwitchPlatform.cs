using UnityEngine;

public class SwitchPlatform : MonoBehaviour
{
    public SwitchState switchState;


    [SerializeField] private Collider2D colliderThis;

    private void Awake()
    {
        colliderThis = GetComponent<Collider2D>();
        Switcher.changeSwitchState += CheckForConditions;
        if (switchState != Switcher.currentSwitch)
            SleepCondition();
        else
            ActiveCondition();
    }

    protected void ActiveCondition()
    {
        colliderThis.enabled = true;
    }

    protected void SleepCondition()
    {
        colliderThis.enabled = false;
    }

    public void CheckForConditions(SwitchState switcher)
    {
        if (switcher == switchState)
            ActiveCondition();
        else
            SleepCondition();
    }
}
