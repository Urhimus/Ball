using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Scripting;

public class GCManager : MonoBehaviour
{
    private void Start()
    {
        GarbageCollector.GCMode = GarbageCollector.Mode.Manual;
        GC.Collect();
    }

    private void OnDisable()
    {
        GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
    }
}
