using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIcon : MonoBehaviour
{
    [SerializeField] private GameObject off, on;

    private void Awake()
    {
        if (AudioListener.volume == 0)
        {
            off.SetActive(false);
            on.SetActive(true);
        } else
        {
            off.SetActive(true);
            on.SetActive(false);
        }
    }
}
