using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BestTime : MonoBehaviour, IDataPersistence
{
    TextMeshProUGUI textMeshPro;
    float text;
    public Timer timer;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (text == 0)
            textMeshPro.text = "";
        else
            textMeshPro.text = "Best time: " + text.ToString("0.00");
        GameStateMachine.setGameState += FinishLevel;
    }
    public void LoadData(GameData gameData) {
        gameData.bestTimeByLevels.TryGetValue(SceneManager.GetActiveScene().name, out text);
    }

    public void SaveData(ref GameData gameData) { }

    void FinishLevel(GameState to, GameState from, StateArgs args)
    {
        if ((timer.timer < text || text == 0) && to == GameState.Finish)
            textMeshPro.text = "Best time: " + timer.timer.ToString("0.00");
    }

}
