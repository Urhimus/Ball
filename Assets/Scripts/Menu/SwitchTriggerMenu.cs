using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTriggerMenu : MonoBehaviour
{
    private Switcher switcher;

    private void Start()
    {
        switcher = GetComponentInParent<Switcher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            switcher.Switching();
    }
}
