using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StarsButtonsUIMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledStar;

    [SerializeField] private Sprite unlockedLevel;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public void LoadData(GameData gameData, ref bool previousLevelIsCompleted)
    {
        UnlockLevel();

        string difficulty = GetComponentInParent<NameLevelsMenu>().gameObject.name;

        if (gameData.starsCollectedByLevels.TryGetValue(difficulty + "_" + name, out bool[] hasStar))
        {
            for (int i = 0; i < 3; i++)
            {
                if (hasStar[i])
                {
                    stars[i + 1].sprite = filledStar;
                    previousLevelIsCompleted = true;
                }

            }
        }

        void UnlockLevel()
        {
            image.sprite = unlockedLevel;
            button.enabled = true;
            for (int i = 0; i < 3; i++)
            {
                stars[i + 1].enabled = true;
            }
            textMeshPro.enabled = true;
        }
    }

    public void LoadScene()
    {
        string difficulty = GetComponentInParent<NameLevelsMenu>().gameObject.name;
        
        SceneManagerScript.instance.LoadScene(difficulty + "_" + name);
    }

    [ContextMenu("SetVars")]
    public void SetVars()
    {
        stars = GetComponentsInChildren<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = name;
    }
}
