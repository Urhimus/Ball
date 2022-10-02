using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float timeScale = 0.1f;

    [SerializeField] GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Time.timeScale = timeScale;
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopTutorial();
    }

    public void StopTutorial()
    {
        if (tutorial.activeSelf)
        {
            Time.timeScale = 1;
            tutorial.SetActive(false);
        }
    }
}

