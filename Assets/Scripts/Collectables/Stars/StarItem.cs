using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarItem : Star
{
    public int currentCount;
    private int count;

    private TextMeshProUGUI tmpro;

    [SerializeField] private CollectableItem[] items;
    private bool[] active;


    public override void Awake()
    {
        base.Awake();

        items = GetComponentsInChildren<CollectableItem>();
        count = items.Length;
        active = new bool[count];

        for (int i = 0; i < count; i++)
            active[i] = true;

        tmpro = GameObject.Find("CounterTMP").GetComponent<TextMeshProUGUI>();
        tmpro.text = "0/" + count;

        GameObject.Find("ItemForStarUi").GetComponentInChildren<Image>().enabled = true;

        CheckPointsManager.checkPointActivated += SaveItems;
        GameStateMachine.setGameState += ResetItems;
    }

    public override bool ConditionMet()
    {
        if (currentCount == count)
            return true;
        return false;
    }

    void ResetItems(GameState to, GameState from, StateArgs stateArgs)
    {
        if (to == GameState.BeforeStart)
        {
            for (int i = 0, x = currentCount; i < x; i++)
            {
                items[i].gameObject.SetActive(active[i]);
                if (items[i].gameObject.activeSelf)
                    currentCount--;
            }
            UpdateCounter();
        }
    }

    void SaveItems(CheckPoint checkPoint)
    {
        for (int i = 0; i < currentCount; i++)
            active[i] = items[i].gameObject.activeSelf;
    }

    public void UpdateCounter()
    {
        tmpro.text = currentCount + "/" + count;
    }

    public override void Description()
    {
        tmPro.text = count > 1 ? "Collect " + count + " coins" : "Collect " + count + " coin";
    }
}
