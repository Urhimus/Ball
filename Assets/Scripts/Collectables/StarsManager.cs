using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StarsManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Star[] stars;
    public static Action<bool[]> starsCollected;

    private void Awake()
    {
        stars = GetComponentsInChildren<Star>();
    }

    private void Start()
    {
        ApplyTextOnStars();

    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData.starsCollectedByLevels == null)
            gameData.starsCollectedByLevels = new SerialiizableDictionary<string, bool[]>();

        string sceneName = SceneManager.GetActiveScene().name;
        bool[] conditionMet = new bool[3]
        {
        stars[0].ConditionMet(), stars[1].ConditionMet(), stars[2].ConditionMet()
        };

        bool[] currentStars = new bool[] {false, false, false};
        if (gameData.starsCollectedByLevels.ContainsKey(sceneName))
        {
            gameData.starsCollectedByLevels.TryGetValue(sceneName, out currentStars);
            for (int i = 0; i < currentStars.Length; i++)
                conditionMet[i] = currentStars[i] ? true : conditionMet[i];
            gameData.starsCollectedByLevels.Remove(sceneName);
        }

        gameData.starsCollectedByLevels.Add(sceneName, conditionMet);

        starsCollected?.Invoke(conditionMet);
    }

    public void LoadData(GameData gameData) { }

    private void OnDisable()
    {
        starsCollected = null;
    }

    [ContextMenu("Apply text on stars")]
    public void ApplyTextOnStars ()
    {
        foreach (Star s in stars)
        {
            s.Description();
        }
    }
}
