using UnityEngine;

public class StarTimer : Star
{
    [SerializeField] private float timeForLevelCompletion;
    private Timer timer;

    public override void Awake()
    {
        base.Awake();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    public override bool ConditionMet()
    {
        if (timeForLevelCompletion == 0)
            return true;
        if (timer.timer < timeForLevelCompletion)
            return true;
        return false;
    }


    public override void Description()
    {
        tmPro.text = timeForLevelCompletion > 0 ? "Complete within " + timeForLevelCompletion + " seconds" : "Reach the finish";
    }
}
