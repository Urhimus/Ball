using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI tmPro;
    [HideInInspector]
    public float timer;

    private void Start()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
        GameStateMachine.setGameState += RestartTheLevel;
    }

    private void Update()
    {
        if (GameStateMachine.instance.gameStateCurrent == GameState.AfterStart)
        {
            timer += Time.deltaTime;
            tmPro.text = timer.ToString("0.00");
        }
    }

    public void SaveData(ref GameData gameData) 
    {
        if (gameData.bestTimeByLevels == null)
            gameData.bestTimeByLevels = new SerialiizableDictionary<string, float>();

        string sceneName = SceneManager.GetActiveScene().name;

        float currentBestTime = timer;
        if (gameData.bestTimeByLevels.ContainsKey(sceneName))
        {
            gameData.bestTimeByLevels.TryGetValue(sceneName, out currentBestTime);
            gameData.bestTimeByLevels.Remove(sceneName);

            if (currentBestTime > timer || currentBestTime == 0)
            {
                currentBestTime = timer;
            }
        }
        gameData.bestTimeByLevels.Add(sceneName, currentBestTime);
    }

    public void LoadData(GameData data)
    {

    }

    void RestartTheLevel(GameState to, GameState from, StateArgs args)
    {
        if (to == GameState.BeforeStart && args.beginFromStart)
            timer = 0;
        tmPro.text = timer.ToString("0.00");
    }
}
