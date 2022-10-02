using System.Collections.Generic;
using UnityEngine;

public class SetFps : MonoBehaviour
{
    private void Awake()
    {
        Set60FPS();
    }

    [ContextMenu("Set60FPS")]
    public void Set60FPS()
    {
        Application.targetFrameRate = 60;
    }

    [ContextMenu("Set30FPS")]
    public void Set30FPS()
    {
        Application.targetFrameRate = 30;
    }
}
