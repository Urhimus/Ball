using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UsableItemActivating : MonoBehaviour
{
    [SerializeField] private Button pressButton;
    
    bool buttonPressed;
    [SerializeField] float delayForButtonPressing;

    public IEnumerator ButtonPressed()
    {
        buttonPressed = true;
        yield return new WaitForSeconds(delayForButtonPressing);
        buttonPressed = false;
    }

    public void ButtonPressedEvent() {
        StopCoroutine(ButtonPressed());
        buttonPressed = false;
        StartCoroutine(ButtonPressed());
    }

    void RemoveEvent()
    {
        StopCoroutine(ButtonPressed());
        buttonPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out UsableItem item))
        {
            item.OnPlayer(true);
            if (!item.autoUse)
            {
                if (buttonPressed)
                {
                    item.Action();
                    StopCoroutine(ButtonPressed());
                    buttonPressed = false;
                }
                else
                {
                    pressButton.onClick.AddListener(item.Action);
                    pressButton.onClick.AddListener(RemoveEvent);

                }
            }
            else
                item.Action();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out UsableItem item))
        {
            if (!item.autoUse) {
                pressButton.onClick.RemoveListener(item.Action);
                pressButton.onClick.RemoveListener(RemoveEvent);
            }

            pressButton.onClick.AddListener(() => ButtonPressed());
            item.OnPlayer(false);
        }
    }
}
