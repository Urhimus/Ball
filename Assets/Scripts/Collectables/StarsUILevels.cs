using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarsUILevels : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Image[] stars = new Image[3];
    [SerializeField] private Sprite emptyStar, filledStar;

    private void Awake()
    {
        stars = GetComponentsInChildren<Image>();
        StarsManager.starsCollected += StarsCollectedEvent;
    }

    public void LoadData(GameData gameData)
    {
        if (gameData.starsCollectedByLevels.TryGetValue(SceneManager.GetActiveScene().name, out bool[] hasStar))
        {
            for (int i = 0; i < 3; i++)
            {
                stars[i].sprite = hasStar[i] ? filledStar : emptyStar;
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {

    }

    private void StarsCollectedEvent(bool[] conditionMet)
    {
        for (int i = 0; i < 3; i++)
        {
            stars[i].sprite = conditionMet[i] ? filledStar : emptyStar;
        }
    }
}
