using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLockOnButtons : MonoBehaviour, IDataPersistence
{
    private StarsButtonsUIMenu[] buttons;
    private bool previousLevelIsCompleted;

    private void Awake()
    {
        buttons = GetComponentsInChildren<StarsButtonsUIMenu>();
        previousLevelIsCompleted = false;
    }

    public void LoadData(GameData gameData)
    {
        foreach(StarsButtonsUIMenu button in buttons)
        {
            if (button.name == "1")
                previousLevelIsCompleted = true;

            if (previousLevelIsCompleted)
            {
                previousLevelIsCompleted = false;
                button.LoadData(gameData, ref previousLevelIsCompleted);
            }
        }
    }

    public void SaveData(ref GameData gameData) { }
}
