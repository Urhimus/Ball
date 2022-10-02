using UnityEngine;
using UnityEngine.UI;

public class RateUs : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite emptyStar, filledStar;

    public void SetStars(int countOfFilledStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (i >= countOfFilledStars)
                stars[i].sprite = emptyStar;
            else
                stars[i].sprite = filledStar;
        }
    }

    public void OpenMarket()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
