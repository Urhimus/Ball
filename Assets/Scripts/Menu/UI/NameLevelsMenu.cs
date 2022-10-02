using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameLevelsMenu : MonoBehaviour
{

    [ContextMenu("NameChildren")]
    public void NameChildren()
    {
        var children = GetComponentsInChildren<StarsButtonsUIMenu>();
        for (int i = 0; i < children.Length; i++)
        {
            var ii = i + 1;
            children[i].name = ii.ToString();
            children[i].SetVars();
        }
    }
}
