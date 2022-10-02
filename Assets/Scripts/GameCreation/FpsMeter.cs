using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsMeter : MonoBehaviour
{

    private int avgFrameRate;
    private TextMeshProUGUI tmpro;
    private float current;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        current = (int)(1f / Time.unscaledDeltaTime);
        tmpro.text = current + " FPS";
    }
}
