using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject firstPanel, difficulties, easy, medium, hard;

    private void Start()
    {
        firstPanel.SetActive(true);
        difficulties.SetActive(false);
        easy.SetActive(false);
        medium.SetActive(false);
        hard.SetActive(false);
    }
}
