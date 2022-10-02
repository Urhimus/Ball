using UnityEngine.UI;
using UnityEngine;

public class ButtonsClickEmul : MonoBehaviour
{

    [SerializeField] Button action, areaAction, switcher, pause, reset, resume;

    public KeyCode actionKey, areaActionKey, switcherKey, pauseKey, resetKey;

    void Update()
    {
        if (action != null && Input.GetKeyDown(actionKey))
            action.onClick.Invoke();
       // if (areaAction.gameObject.activeSelf && Input.GetKeyDown(areaActionKey))
       //     areaAction.onClick.Invoke();
        if (switcher.gameObject.activeSelf && Input.GetKeyDown(switcherKey))
            switcher.onClick.Invoke();
        if (pause.gameObject.activeSelf && Input.GetKeyDown(pauseKey))
            pause.onClick.Invoke();
        if (reset.gameObject.activeSelf && Input.GetKeyDown(resetKey))
            reset.onClick.Invoke();
    }
}
