using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private StarItem parent;

    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        parent = GetComponentInParent<StarItem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            parent.currentCount++;
            parent.UpdateCounter();
            audioSource.Play();
            gameObject.SetActive(false);
        }
    }
}
